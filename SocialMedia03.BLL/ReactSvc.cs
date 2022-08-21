using SocialMedia03.Common.BLL;
using SocialMedia03.Common.Res;
using SocialMedia03.DAL;
using SocialMedia03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia03.BLL
{
    public class ReactSvc : GenericSvc<ReactRep, React>
    {
        private UserSvc UserSvc;

        public ReactSvc()
        {
            UserSvc = new UserSvc();
        }

        public override React Get(int id)
        {
            return _rep.Get(id);
        }

        public HashSet<React> GetReactsByPost(int postId)
        {
            HashSet<React> rs = _rep.GetReactsByPost(postId);  
            foreach(var r in rs)
            {
                r.User = UserSvc.Get(r.UserId);
            }

            return rs;
        }

        public HashSet<React> GetReactsByComment(int commentId)
        {
            HashSet<React> rs = _rep.GetReactsByComment(commentId);
            foreach (var r in rs)
            {
                r.User = UserSvc.Get(r.UserId);
            }

            return rs;
        }

        public bool CreateReact(int? postId, int? commentId, int userId)
        {
            React react = new React();
            react.PostId = postId;
            react.UserId = userId;
            react.CommentId = commentId;
            react.CreatedDate = DateTime.Now;
            react.Type = 1;

            return _rep.CreateReact(react);
        }

        public bool DeleteReact(int? postId, int? commentId, int userId)
        {
            React react;
            if (postId != null) 
            {
                react = _rep.GetReactPost((int)postId, userId);
            } else
            {
                react = _rep.GetReactComment((int)commentId, userId);
            }

            return _rep.DeleteReact(react);
        }
    }
}
