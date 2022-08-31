﻿using Microsoft.AspNetCore.Mvc;
using SocialMedia03.BLL;
using SocialMedia03.DAL.Models;

namespace SocialMedia03.WEB.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        private AdminService adminService = new AdminService();

        [Route("")]
        public IActionResult Stats([FromQuery]int? month=0,[FromQuery]int? year=0)
        {

            return View("Stats", adminService.CountStats((int)month, (int)year));
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
