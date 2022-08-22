using SocialMedia03.Common.BLL;
using SocialMedia03.DAL;
using SocialMedia03.DAL.Models;
using BC = BCrypt.Net.BCrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool RegisterNewUser(User user, string password)
        {
            Guid uuid = Guid.NewGuid();
            user.Uuid = uuid.ToString();
            user.Password = BC.EnhancedHashPassword(password);
            user.UserRole = "ROLE_USER";
            user.Enable = true;
            user.CreatedDate = DateTime.Now;

            return _rep.RegisterNewUser(user);
        }
    }
}
