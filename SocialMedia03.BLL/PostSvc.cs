using SocialMedia03.Common.BLL;
using SocialMedia03.Common.Req;
using SocialMedia03.Common.Res;
using SocialMedia03.Common.Utils;
using SocialMedia03.DAL;
using SocialMedia03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public Post Get(int id)
        {
            try
            {
                Post p = _rep.GetSingle<Post>(id);
                if (p != null)
                {
                    p.User = UserSvc.Get(p.UserId);
                    p.Reacts = ReactSvc.GetReactsByPost(p.Id).ToHashSet();
                    p.CommentSetLength = CommentSvc.CountCommentByPost(p.Id);
                }

                return p;
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
           
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

        public IQueryable<Post> Get(int page, string kw)
        {
            IQueryable<Post> rs = _rep.Get(page, kw);
            foreach (var p in rs)
            {
                p.User = UserSvc.Get(p.UserId);
                p.Reacts = ReactSvc.GetReactsByPost(p.Id).ToHashSet();
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
            p.Hashtag = req.Hashtag;

            if (_rep.Create(p) == true) 
            {
                p.User = creator;
                return p;
            }

            return null;
        }

        public Post Update(int currentPostId, PostReq req, User creator)
        {
            Post currentPost = this.Get(currentPostId);
            currentPost.Content = req.Content;
            currentPost.Hashtag = req.Hashtag;
            if (currentPost.Image != req.ImageUrl && !String.IsNullOrEmpty(currentPost.Image))
            {
                var public_id = currentPost.Image.Substring(currentPost.Image.LastIndexOf("public_id=") + 10);
                CloudinaryUtils.DeleteImage(public_id);
                currentPost.Image = req.ImageUrl;
            }
            if (_rep.Update(currentPost) == true)
                return currentPost;
            return null;
        }

        public bool Delete(int postId)
        {
            Post p = this.Get(postId);

            return _rep.Delete(p);
        }

        public Post FindPostByComment(int commentId)
        {
            return _rep.FindPostByComment(commentId);
        }

        public IQueryable<Post> SearchByContent(string kw, int page)
        {
            var p = _rep.Get(page, kw);
           
            return p;
        }

        public List<Post> SearchByHashtag(string hashtag, int page)
        {
            var posts = _rep.SearchByHashtag(hashtag, page).ToList();
            try
            {
                posts.ForEach(p =>
                {
                    p.User = UserSvc.Get(p.UserId);
                    p.Reacts = ReactSvc.GetReactsByPost(p.Id).ToHashSet();
                    p.CommentSetLength = CommentSvc.CountCommentByPost(p.Id);
                });
               
              
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

            return posts;
        }

        public HashSet<Post> GetPostByUser(int userId, int page)
        {
            HashSet<Post> rs = _rep.GetPostByUser(userId, page);
            foreach (var p in rs)
            {
                p.User = UserSvc.Get(p.UserId);
                p.Reacts = ReactSvc.GetReactsByPost(p.Id).ToHashSet();
                p.CommentSetLength = CommentSvc.CountCommentByPost(p.Id);
            }

            return rs;
        }

    }
}
