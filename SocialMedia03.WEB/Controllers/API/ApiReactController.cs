using Microsoft.AspNetCore.Mvc;
using SocialMedia03.BLL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
#pragma warning disable CS8629 // Nullable value type may be null.

namespace SocialMedia03.WEB.Controllers.API
{
    [Route("api/react")]
    [ApiController]
    public class ApiReactController : ControllerBase
    {
        private ReactSvc ReactSvc = new ReactSvc();
        // GET: api/react
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        // GET api/react/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/react/add?postId={postId}&commentId={commentId}
        [HttpPost("add")]
        public void CreateReact(int? postId, int? commentId)
        {

            int currentUserId = (int)(HttpContext.Session.GetInt32("currentUserId") == null ? 0
                 : HttpContext.Session.GetInt32("currentUserId"));

            ReactSvc.CreateReact(postId, commentId, currentUserId);
        }

        // PUT api/react/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/react/delete?postId={postId}&commentId={commentId}
        [HttpDelete("delete")]
        public void Delete(int? postId, int? commentId)
        {
            int currentUserId = (int)(HttpContext.Session.GetInt32("currentUserId") == null ? 0
                : HttpContext.Session.GetInt32("currentUserId"));

            ReactSvc.DeleteReact(postId, commentId, currentUserId);
        }
    }
}
