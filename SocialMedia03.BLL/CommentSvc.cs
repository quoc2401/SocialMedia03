using SocialMedia03.Common.BLL;
using SocialMedia03.Common.Req;
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
    public class CommentSvc : GenericSvc<CommentRep, Comment>
    {
        private UserSvc UserSvc;
        private ReactSvc ReactSvc;

        public CommentSvc()
        {
            UserSvc = new UserSvc();
            ReactSvc = new ReactSvc();
        }

        public Comment Get(int id)
        {
            Comment c = _rep.GetSingle<Comment>(id);
            c.User = base.Get<User>(c.UserId);
            c.Reacts = ReactSvc.GetReactsByComment(c.Id).ToHashSet();
            c.CommentSetLength = _rep.CountReplies(c.Id);
            if (c.ParentId != null)
                c.Parent = this.Get((int)c.ParentId);
            return c;
        }

        public IQueryable<Comment> GetCommentsByPost(int postId, int? page)
        {
            IQueryable<Comment> rs = _rep.GetCommentsByPost(postId, page);
            foreach (var c in rs)
            {
                c.User = base.Get<User>(c.UserId);
                c.Reacts = ReactSvc.GetReactsByComment(c.Id).ToHashSet();
                c.CommentSetLength = _rep.CountReplies(c.Id);
            }

            return rs;
        }

        public int CountCommentByPost(int postId)
        {
            return _rep.CountCommentByPost(postId);
        }

        public IQueryable<Comment> GetCommentReplies(int commentId, int? page)
        {
            IQueryable<Comment> rs = _rep.GetCommentReplies(commentId, page);
            foreach (var c in rs)
            {
                c.User = base.Get<User>(c.UserId);
                c.Reacts = ReactSvc.GetReactsByComment(c.Id).ToHashSet();
                c.CommentSetLength = _rep.CountReplies(c.Id);
            }

            return rs;
        }

        public bool Delete(int id)
        {
            return base.Delete(base.Get<Comment>(id));
        }

        public Comment Create(CommentReq req, User creator)
        {
            try
            {
                Comment c = new Comment();
                c.Content = req.Content;
                c.CreatedDate = DateTime.Now;
                c.UserId = creator.Id;
                c.PostId = req.PostId;
                c.ParentId = req.CommentId;
                if (_rep.Create(c) == true)
                {
                    if (c.PostId != null)
                    {
                        c.Post = new Post();
                        c.Post.User = UserSvc.GetUserByPost((int)c.PostId);
                    }
                    else
                    {
                        c.Parent = new Comment();
                        c.Parent.User = UserSvc.getUserByComment((int)c.ParentId);
                    }

                    c.User = creator;
                    return c;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
            return null;
        }

        public Comment Update(int currentCommentId, CommentReq req)
        {
            Comment currentComment = this.Get(currentCommentId);
            currentComment.User = base.Get<User>(currentComment.UserId);
            currentComment.Reacts = ReactSvc.GetReactsByComment(currentComment.Id).ToHashSet();
            currentComment.Content = req.Content;
            currentComment.CreatedDate = DateTime.Now;
            
            if (_rep.Update(currentComment) == true)
                return currentComment;
            return null;
        }
    }
}
