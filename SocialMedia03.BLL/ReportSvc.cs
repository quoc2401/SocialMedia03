using SocialMedia03.Common.BLL;
using SocialMedia03.Common.Req;
using SocialMedia03.DAL;
using SocialMedia03.DAL.Models;

namespace SocialMedia03.BLL
{
    public class ReportSvc : GenericSvc<ReportRep, Report>
    {
        private ReportRep reportRep;
        private UserRep userRep;
        private PostRep postRep;
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
            
            return _rep.Create(report);
        }

        public IQueryable<Report> GetPostReport()
        {

            return _rep.GetPostReport();
        }

        public IQueryable<Report> GetUserReport()
        {

            return _rep.GetUserReport();
        }
    }
}