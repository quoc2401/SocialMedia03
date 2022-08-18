using SocialMedia03.Common.BLL;
using SocialMedia03.Common.Res;
using SocialMedia03.DAL;
using SocialMedia03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia03.BLL
{
    public class PostSvc : GenericSvc<PostRep, Post>
    {
        public PostSvc()
        {
        }

        public override SingleRes Get(int id)
        {
            var res = new SingleRes();
            res.Data = _rep.Get(id);

            return res;
        }

        public List<Post> GetAll()
        {


            return _rep.All.ToList();
        }


    }
}
