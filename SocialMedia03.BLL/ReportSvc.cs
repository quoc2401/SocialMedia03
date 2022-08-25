using SocialMedia03.Common.BLL;
using SocialMedia03.Common.Req;
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
    public class ReportSvc : GenericSvc<ReportRep, Report>
    {
        private ReportRep reportRep;
        public ReportSvc()
        {
            reportRep = new ReportRep();
        }

        public SingleRes CreateReport(ReportRequest reportReq)
        {
            var res = new SingleRes();
            Report report = new Report();
            report.TargetUserId = reportReq.TargetPostId;
            report.TargetPostId = reportReq.TargetPostId;
            report.Reason = reportReq.Reason;
            report.Details = reportReq.Details;
            res = _rep.CreateReport(report);
            return res;
        }
        
    }
}
