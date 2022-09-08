using Microsoft.AspNetCore.Mvc;
using SocialMedia03.BLL;
using SocialMedia03.DAL.Models;
using SocialMedia03.Common.DAL;
using SocialMedia03.Common.Req;

namespace SocialMedia03.WEB.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        private ReportSvc reportSvc = new ReportSvc();

        [Route("")]
        public IActionResult Stats()
        {
            return View("Stats");
        }

        [Route("post-reports")]
        public IActionResult PostReports()
        {
            List<Report> reportPostList = new List<Report>();
            reportPostList = reportSvc.GetReport();

            return View("Reports", reportPostList);
            
        }

        [Route("user-reports")]
        public IActionResult UserReports()
        {
            List<Report> reportUserList = new List<Report>();
            reportUserList = reportSvc.GetReport();

            return View("Reports", reportUserList);
        }

        
    }
}
