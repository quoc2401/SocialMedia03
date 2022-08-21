using SocialMedia03.Common.DAL;
using SocialMedia03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia03.DAL
{
    public class UserRep : GenericRep<smdbContext, User>
    {
        public override User Get(int id)
        {
            return base.Context.Set<User>().Where(p => p.Id == id).SingleOrDefault();
        }


    }
}
