using SocialMedia03.Common.BLL;
using SocialMedia03.DAL;
using SocialMedia03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia03.BLL
{
    public class CommentSvc:GenericSvc<CommentRep, Comment>
    {
        private UserSvc UserSvc;
        private ReactSvc ReactSvc;

        public CommentSvc()
        {
            UserSvc = new UserSvc();
            ReactSvc = new ReactSvc();
        }

        public override Comment Get(int id)
        {
            return _rep.Get(id);
        }

        public HashSet<Comment> GetCommentsByPost(int postId, int? page) 
        {
            HashSet<Comment> rs = _rep.GetCommentsByPost(postId, page);
            foreach(var c in rs)
            {
                c.User = UserSvc.Get(c.UserId);
                c.Reacts = ReactSvc.GetReactsByComment(c.Id);
            }

            return rs;
        }

        public int CountCommentByPost(int postId)
        {
            return _rep.CountCommentByPost(postId);
        }
    }
}
