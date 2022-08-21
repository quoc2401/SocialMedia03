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
    public class UserSvc : GenericSvc<UserRep, User>
    {
        public User Get(int id)
        {
            return _rep.Get(id);
        }
    }
}
