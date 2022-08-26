using Microsoft.AspNetCore.Mvc;
using SocialMedia03.BLL;
using SocialMedia03.DAL.Models;
using System.Diagnostics;

namespace SocialMedia03.WEB.Controllers
{
    [Route("post")]
    public class PostController : Controller
    {
        private PostSvc postSvc = new PostSvc();
        private NotifSvc notifSvc = new NotifSvc();

        [Route("{postId}")]
        public IActionResult PostPage([FromRoute]int postId, [FromQuery]int? notif_id,
            [FromQuery] string? notif_type, [FromQuery(Name="ref")] string? refer)
        {
            Debug.WriteLine(notif_id);
            if (refer.Equals("notif") && notif_id != null)
                notifSvc.readNotif((int)notif_id);

            Post model = postSvc.Get(postId);
            return View("Post", model);
        }
    }
}
