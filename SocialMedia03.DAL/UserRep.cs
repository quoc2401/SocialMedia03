using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

        public User GetUserByEmail(string email)
        {
            return base.Context.Set<User>().Where(u => u.Email == email).SingleOrDefault();
        }

        public bool RegisterNewUser(User user)
        {
            try
            {
                base.Context.Entry(user).State = user.Id == 0 ?
                    EntityState.Added : EntityState.Modified;
                base.Context.SaveChanges();

                return true;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }


    }
}
