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
                base.Context.Entry(p).State = EntityState.Deleted;

                base.Context.SaveChanges();

                return true;
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.StackTrace);
                return false;
            }
        }
    }
}
