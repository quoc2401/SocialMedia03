using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using SocialMedia03.Common.Req;
using SocialMedia03.Common.Res;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialMedia03.BLL;


namespace SocialMedia03.WEB.Controllers
{
    public class ReportController : ControllerBase
    {
        private ReportSvc reportSvc;
        public ReportController()
        {
            reportSvc = new ReportSvc();
        }
        [HttpPost("create-report")]
        public IActionResult CreateReport([FromBody] ReportRequest reportReq)
        {
            var res = reportSvc.CreateReport(reportReq);
            return Ok(res);
        }
        

    }
}
