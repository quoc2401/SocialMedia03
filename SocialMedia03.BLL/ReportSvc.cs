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

        
        public bool CreateReport(int uID, ReportReq req)
        {
            Report report = new Report();


            report.UserId = uID;
            report.TargetUserId = req.UserId;
            report.TargetPostId = req.PostId;
            report.Reason = req.Reason;
            report.IsSolve = false;
            
            return _rep.CreateReport(report);
        }

        public List<Report> GetPostReport()
        {
            List<Report> reportPostList = new List<Report>();

            reportPostList = _rep.GetPostReport();

            return reportPostList;
        }

        public List<Report> GetUserReport()
        {
            List<Report> reportUserList = new List<Report>();

            reportUserList = _rep.GetUserReport();

            return reportUserList;
        }
    }
}