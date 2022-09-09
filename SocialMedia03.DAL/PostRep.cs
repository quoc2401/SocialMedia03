using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SocialMedia03.Common;
using SocialMedia03.Common.DAL;
using SocialMedia03.DAL.Models;
using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia03.DAL
{
    public class PostRep : GenericRep<smdbContext, Post>
    {
        public Post Get(int id)
        {
            return base.Context.Set<Post>().Where(p => p.Id == id).FirstOrDefault();
           
        }

        public HashSet<Post> Get(int page, string kw)
        {
            HashSet<Post> rs = new HashSet<Post>();
            int size = configs.POST_PAGE_SIZE;
            if (kw == null)
                kw = "";

            if (page > 0)
            {
                int start = (page - 1) * size;

                rs = base.Context.Set<Post>().Where(p => p.Content.Contains(kw)).AsEnumerable().Skip(start).Take(size).ToHashSet();
            }
            else
            {
                rs = base.Context.Set<Post>().Where(p => p.Content.Contains(kw)).ToHashSet();
            }

            return rs;
        }

        public bool Create(Post p)
        {
            try
            {
                base.Context.Entry(p).State = p.Id == 0 ?
                    EntityState.Added : EntityState.Modified;
                base.Context.SaveChanges();

                return true;
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.StackTrace);
                return false;
            }
        }

        public bool Update(Post p)
        {
            try
            {
                base.Context.Entry(p).State = p.Id == 0 ?
                    EntityState.Added : EntityState.Modified;
                base.Context.SaveChanges();

                return true;
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.StackTrace);
                return false;
            }
        }

        public bool Delete(Post p)
        {
            if (p == null)
                return true;
            try
            {
                base.Context.Database.ExecuteSqlRaw("Delete Post Where id={0}", p.Id);

                base.Context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                if (e is DbUpdateException)
                    return true;
                Debug.WriteLine(e.StackTrace);
                return false;
            }
        }

        public Post FindPostByComment(int commentId)
        {
            int postId;
            Comment comment = Context.Comments.Where(c => c.Id == commentId).SingleOrDefault();
            Post result;

            if (comment.PostId == null && comment.ParentId != null)
                result = FindPostByComment((int)comment.ParentId);
            else
                result = Context.Posts.Where(p => p.Id == comment.PostId).SingleOrDefault(); ;
            return result;
        }

        public int CountPost(int month, int year)
        {
            int count;
            if (year == 0)
                count = Context.Posts.Count();
            else if (month >= 1 && month <= 12)
                count = Context.Posts.Where(u => u.CreatedDate.Month == month && u.CreatedDate.Year == year).Count();
            else
                count = Context.Posts.Where(u => u.CreatedDate.Year == year).Count();
            return count;
        }

       

        public List<Post> SearchByHashtag(string hashtag, int page)
        {
            List<Post> rs = new List<Post>();
            int size = configs.POST_PAGE_SIZE;
            if (hashtag == null)
                hashtag = "";

            if (page > 0)
            {
                int start = (page - 1) * size;

                rs = base.Context.Set<Post>().Where(p => p.Hashtag.Contains(hashtag+" ")).AsEnumerable().Skip(start).Take(size).ToList();
            }
            else
            {
                rs = base.Context.Set<Post>().Where(p => p.Hashtag.Contains(hashtag)).ToList();
            }

            return rs;
        }

        public HashSet<Post> GetPostByUser(int userId, int page)
        {
            HashSet<Post> rs = new HashSet<Post>();
            int size = configs.POST_PAGE_SIZE;

            if (page > 0)
            {
                int start = (page - 1) * size;

                rs = base.Context.Set<Post>().Where(p => p.UserId == userId).AsEnumerable().Skip(start).Take(size).ToHashSet();
            }
            else
            {
                rs = base.Context.Set<Post>().Where(p => p.UserId == userId).ToHashSet();
            }

            return rs;
        }
    }
}
