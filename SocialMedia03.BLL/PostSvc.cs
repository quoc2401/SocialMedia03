using SocialMedia03.Common.BLL;
using SocialMedia03.Common.Req;
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
    public class PostSvc : GenericSvc<PostRep, Post>
    {
        private UserSvc UserSvc;
        private ReactSvc ReactSvc;
        private CommentSvc CommentSvc;
        public PostSvc()
        {
            ReactSvc = new ReactSvc();
            UserSvc = new UserSvc();
            CommentSvc = new CommentSvc();
        }

        public override Post Get(int id)
        {
            return _rep.Get(id);
        }

        public HashSet<Post> GetAll()
        {
            HashSet<Post> rs = _rep.All.ToHashSet();
            //foreach(var p in rs)
            //{
            //    p.User = UserSvc.Get(p.UserId);
            //}

            return rs;
        }

        public HashSet<Post> Get(int page, string kw)
        {
            HashSet<Post> rs = _rep.Get(page, kw);
            foreach (var p in rs)
            {
                p.User = UserSvc.Get(p.UserId);
                p.Reacts = ReactSvc.GetReactsByPost(p.Id);
                p.CommentSetLength = CommentSvc.CountCommentByPost(p.Id);
            }

            return rs;
        }

        public Post Create(PostReq req, User creator)
        {
            Post p = new Post();
            p.Content = req.Content;
            p.Image = req.ImageUrl;
            p.UserId = creator.Id;
            p.User = creator;
            p.Hashtag = req.Hashtag;
            if (_rep.Create(p) == true)
                return p;

            return null;
        }
    }
}
