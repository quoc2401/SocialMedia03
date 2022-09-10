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

        public IQueryable<React> GetReactsByPost(int postId)
        {
            IQueryable<React> rs = _rep.GetReactsByPost(postId);  
            foreach(var r in rs)
            {
                r.User = base.Get<User>(r.UserId);
            }

            return rs;
        }

        public IQueryable<React> GetReactsByComment(int commentId)
        {
            IQueryable<React> rs = _rep.GetReactsByComment(commentId);
            foreach (var r in rs)
            {
                r.User = base.Get<User>(r.UserId);
            }

            return rs;
        }

        public User CreateReact(int? postId, int? commentId, int userId)
        {
            React react = new React();
            react.PostId = postId;
            react.UserId = userId;
            react.CommentId = commentId;
            react.CreatedDate = DateTime.Now;
            react.Type = 1;
            if (_rep.Create(react)) 
            {
                if (postId != null)
                {
                    return UserSvc.GetUserByPost((int)postId);
                }
                else
                    return UserSvc.getUserByComment((int)commentId);
            }
            return null;
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

            return _rep.Delete(react);
        }
    }
}
