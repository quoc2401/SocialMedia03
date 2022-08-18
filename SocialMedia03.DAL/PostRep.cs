using SocialMedia03.Common.DAL;
using SocialMedia03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia03.DAL
{
    public class PostRep : GenericRep<smdbContext, Post>
    {
        public override Post Get(int id)
        {
            return base.Context.Set<Post>().Where(p => p.Id == id).Single();
        }


    }
}
