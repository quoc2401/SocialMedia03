using SocialMedia03.Common.DAL;
using SocialMedia03.DAL.Models;
using SocialMedia03.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialMedia03.Common.Res;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http.Features;

namespace SocialMedia03.DAL
{
    public class ReportRep: GenericRep<smdbContext, Report>
    {
        public ReportRep()
        { }

        //override
        public IQueryable<Report> GetPostReport()
        { 
            return base.Get<Report>(report => report.TargetPostId != null);
        }

        public IQueryable<Report> GetUserReport()
        {

            return base.Get<Report>(report => report.TargetUserId != null);
        }
        // 
    }
}
