using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using SocialMedia03.Common.Req;
using SocialMedia03.Common.Res;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialMedia03.BLL;
using SocialMedia03.DAL.Models;


namespace SocialMedia03.WEB.Controllers
{
    public class ReportController : Controller
    {
        private ReportSvc reportSvc;
        public ReportController()
        {
            reportSvc = new ReportSvc();
        }


    }
}
