using SocialMedia03.Common;
using SocialMedia03.Common.DAL;
using SocialMedia03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia03.DAL
{
    public class CommentRep : GenericRep<smdbContext, Comment>
    {
        public override Comment Get(int id)
        {
            return base.Context.Set<Comment>().Where(c => c.Id == id).SingleOrDefault();
                
        }

        public HashSet<Comment> GetCommentsByPost(int postId, int? page)
        {
            HashSet<Comment> rs = new HashSet<Comment>();

            int size = configs.POST_PAGE_SIZE;
            if (page > 0)
            {
                int start = ((int)page - 1) * size;

                rs = base.Context.Set<Comment>().Where(c => c.PostId == postId).AsEnumerable().Skip(start).Take(size).ToHashSet();
            }
            else
            {
                rs = base.Context.Set<Comment>().Where(c => c.PostId == postId).ToHashSet();
            }

            return rs;
                
        }

        public int CountCommentByPost(int postId)
        {
            return base.Context.Set<Comment>().Where(c => c.PostId == postId).Count();
        }

        public int CountReplies(int commentId)
        {
            return base.Context.Set<Comment>().Where(c => c.ParentId == commentId).Count();
        }
    }
}
