using Microsoft.AspNetCore.Mvc;

namespace SocialMedia03.WEB.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        [Route("login")]
        public IActionResult Login()
        {
            return View("Login");
        }

        [Route("register")]
        public IActionResult Register()
        {
            return View("Register");
        }
    }
}
