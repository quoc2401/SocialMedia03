using Microsoft.AspNetCore.Mvc;

namespace SocialMedia03.WEB.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        [Route("")]
        public IActionResult Stats()
        {
            return View("Stats");
        }

        [Route("post-reports")]
        public IActionResult PostReports()
        {
            return View("Reports");
        }

        [Route("user-reports")]
        public IActionResult UserReports()
        {
            return View("Reports");
        }
    }
}
