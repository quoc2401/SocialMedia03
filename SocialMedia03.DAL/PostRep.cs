using SocialMedia03.Common;
using SocialMedia03.Common.DAL;
using SocialMedia03.DAL.Models;
using System;
using System.Collections;
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

        public List<Post> GetPost(int page, string kw)
        {
            List<Post> rs = new List<Post>();
            int size = configs.POST_PAGE_SIZE;
            if (kw == null)
                kw = "";    

            if (page > 0)
            {
                int start = (page - 1) * 20;
                Console.WriteLine("start: " + start);
                Console.WriteLine(size);

                rs = base.Context.Set<Post>().Where(p => p.Content.Contains(kw)).OrderBy(p => p.CreatedDate).Skip(start).Take(size).ToList();
            } else
            {
                rs = base.Context.Set<Post>().Where(p => p.Content.Contains(kw)).ToList();
            }

            return rs;
        }

    }
}
