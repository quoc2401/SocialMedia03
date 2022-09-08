using SocialMedia03.Common.BLL;
using SocialMedia03.Common.Req;
using SocialMedia03.Common.Res;
using SocialMedia03.DAL;
using SocialMedia03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia03.BLL
{
    public class ReportSvc : GenericSvc<ReportRep, Report>
    {
        private ReportRep reportRep;
        private UserRep userRep;
        public PostRep postRep;
        public ReportSvc()
        {
            reportRep = new ReportRep();
            userRep = new UserRep();
            postRep = new PostRep();
        }

        
        public bool CreateReport(int uID, int tID, int pID, string reason, string details)
        {
            Report report = new Report();


            report.Id = uID;
            report.TargetUserId = tID;
            report.TargetPostId = pID;
            report.Reason = reason;
            report.Details = details;
            report.IsSolve = false;
            
            return _rep.CreateReport(report);
        }

        public List<Report> GetReport()
        {
            List<Report> reportList = new List<Report>();
            reportList = _rep.GetReport();

            return reportList;
        }

    }
}