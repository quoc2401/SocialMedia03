using Microsoft.AspNetCore.Mvc;

namespace SocialMedia03.WEB.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        [Route("login")]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UUID") != null)
            {
                return Redirect("/");
            }

            return View("Login");
        }

        [Route("register")]
        public IActionResult Register()
        {
            return View("Register");
        }

        [Route("logout")]
        public IActionResult Logout()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                if (cookie == "SocialMedia03")
                    Response.Cookies.Delete(cookie);
            }
            HttpContext.Session.Clear();

            return Redirect("/account/login");
        }
    }
}
