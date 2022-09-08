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
        private AdminService adminService = new AdminService();
        private UserSvc userSvc = new UserSvc();

        [Route("")]
        public IActionResult Stats([FromQuery]int? month=0,[FromQuery]int? year=0)
        {

            return View("Stats", adminService.CountStats((int)month, (int)year));
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

        [Route("user")]
        public IActionResult User([FromQuery] string? kw)
        {
            return View("User", userSvc.SearchByUser(kw, 0, 0));
        }
    }
}
