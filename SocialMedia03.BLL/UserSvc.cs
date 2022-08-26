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

namespace SocialMedia03.BLL
{
    public class UserSvc : GenericSvc<UserRep, User>
    {

        public User Get(int id)
        {
            return _rep.Get(id);
        }

        public User Authenticate(string email, string password)
        {
            User user = _rep.GetUserByEmail(email.Trim());
            if (user != null && BC.EnhancedVerify(password.Trim(), user.Password.Trim()) == true)
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
            if(String.IsNullOrEmpty(u.Avatar))
            {
                u.Avatar = "https://res.cloudinary.com/dynupxxry/image/upload/v1660532211/non-avatar_nw91c3.png";
            }

            return _rep.RegisterNewUser(u);
        }
    }
}
