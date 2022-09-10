using SocialMedia03.Common.Res;
using SocialMedia03.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia03.BLL
{
    public class AdminSvc
    {
        UserRep uRep = new UserRep();
        PostRep pRep = new PostRep();
        ReactRep rRep = new ReactRep();
        CommentRep cRep = new CommentRep();

        public StatsRes CountStats (int month , int year)
        {
            StatsRes res = new StatsRes();
            res.CountUser = uRep.CountUser(month, year);
            res.CountPost = pRep.CountPost(month, year);
            res.CountReact = rRep.CountReact(month, year);
            res.CountComment = cRep.CountComment(month, year);

            return res;
        }

    }
}
