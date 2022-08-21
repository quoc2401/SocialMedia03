using Microsoft.AspNetCore.Mvc;

namespace SocialMedia03.WEB.Controllers
{
    [Route("user")]
    public class UserPageController : Controller
    {
        [Route("{userId}")]
        public IActionResult UserPage(int? userId)
        {
            return View("UserPage");
        }
    }
}
