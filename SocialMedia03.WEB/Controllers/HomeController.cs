using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialMedia03.BLL;
using SocialMedia03.DAL.Models;
using SocialMedia03.WEB.Models;
using System.Diagnostics;

namespace SocialMedia03.WEB.Controllers
{
    public class HomeController : Controller
    {
        private PostSvc PostSvc = new PostSvc();
        private UserSvc UserSvc = new UserSvc();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            User currentUser = UserSvc.Get(3);
            HttpContext.Session.SetString("currentUser", JsonConvert.SerializeObject(UserSvc.Get(3)));
            HttpContext.Session.SetInt32("currentUserId", currentUser.Id);
            HttpContext.Session.SetString("currentUserLastname", currentUser.Lastname);
            HttpContext.Session.SetString("currentUserFirstname", currentUser.Firstname);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}