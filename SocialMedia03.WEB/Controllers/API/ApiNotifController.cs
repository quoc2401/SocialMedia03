using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia03.BLL;
using System.Diagnostics;

namespace SocialMedia03.WEB.Controllers.API
{
    [Route("api/notif")]
    [ApiController]
    public class ApiNotifController : ControllerBase
    {

        private NotifSvc notifSvc = new NotifSvc();
        // GET: api/notif
        [HttpGet("")]
        public IActionResult GetPosts(int page)
        {
            try
            {
                int currentUserId = (int)HttpContext.Session.GetInt32("currentUserId");
                return Ok(notifSvc.getNotifs(currentUserId, page));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
