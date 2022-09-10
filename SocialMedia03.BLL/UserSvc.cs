using SocialMedia03.Common.BLL;
using SocialMedia03.DAL;
using SocialMedia03.DAL.Models;
using BC = BCrypt.Net.BCrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMedia03.WEB.Models;
using System.Diagnostics;

namespace SocialMedia03.BLL
{
    public class UserSvc : GenericSvc<UserRep, User>
    {

        public User Get(int id)
        {
            return _rep.GetSingle<User>(id);
        }

        public User Authenticate(string email, string password)
        {
            User user = _rep.GetUserByEmail(email.Trim());
            if (user != null && BC.Verify(password.Trim(), user.Password.Trim()))
            {
                return user;
            } 
            else
            {
                return null;
            }
        }

        public bool RegisterNewUser(UserReq req)
        {
            User u = new User(req);
            Guid uuid = Guid.NewGuid();
            u.Uuid = uuid.ToString();
            u.UserRole = "ROLE_USER";
            u.CreatedDate = DateTime.Now;
            if(String.IsNullOrEmpty(u.Avatar))
            {
                u.Avatar = "https://res.cloudinary.com/dynupxxry/image/upload/v1660532211/non-avatar_nw91c3.png";
            }

            return _rep.Create(u);
        }

        public User GetUserByPost(int postId)
        {
            return _rep.GetUserByPost(postId);
        }

        public User getUserByComment(int commentId)
        {
            return _rep.getUserByComment(commentId);
        }

        public List<User> SearchByUser(string kw, int page, int limit)
        {
            var users = _rep.SearchByUser(kw, page, limit).ToList();
          

            return users;
        }

        public bool Delete(int id)
        {
            User u = _rep.GetSingle<User>(id);
            

            return _rep.Delete(u);
        }
    }
}
