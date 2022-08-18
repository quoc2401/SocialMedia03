using Microsoft.AspNetCore.Mvc;
using SocialMedia03.BLL;
using SocialMedia03.WEB.Models;
using System.Diagnostics;

namespace SocialMedia03.WEB.Controllers
{
    public class HomeController : Controller
    {
        private PostSvc postSvc = new PostSvc();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}