using Microsoft.AspNetCore.Mvc;
using SocialMedia03.BLL;
using SocialMedia03.DAL.Models;

namespace SocialMedia03.WEB.Controllers
{
    [Route("user")]
    public class UserPageController : Controller
    {

        private UserSvc userSvc = new UserSvc();

        [Route("{userId}")]
        public IActionResult UserPage(int userId)
        {
            return View("UserPage", userSvc.Get(userId));
        }
    }
}
