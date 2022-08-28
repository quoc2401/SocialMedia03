using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialMedia03.BLL;
using SocialMedia03.Common.Req;
using SocialMedia03.DAL.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialMedia03.WEB.Controllers.API
{
    [Route("api/comment")]
    [ApiController]
    public class ApiCommentController : ControllerBase
    {
        private CommentSvc CommentSvc = new CommentSvc();
        // GET: api/comment?params
        [HttpGet]
        public IActionResult GetCommentByPost(int postId, int? page = 0)
        {
            return Ok(CommentSvc.GetCommentsByPost(postId, page));
        }

        [HttpGet("get-replies")]
        public IActionResult GetCommentReplies(int commentId, int? page = 0)
        {
            return Ok(CommentSvc.GetCommentReplies(commentId, page));
        }

        // GET api/comment/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/comment/add
        [HttpPost("add")]
        public IActionResult Post([FromBody] CommentReq req)
        {
            User currentUser = new User();
            if (HttpContext.Session.GetString("currentUser") != null)
            {
                currentUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("currentUser"));
                Comment res = CommentSvc.Create(req, currentUser);
                if (res != null)
                    return Ok(res);

            }

            return StatusCode(500);
        }

        // PUT api/comment/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/comment/5
        [HttpDelete("delete/{commentId}")]
        public IActionResult Delete([FromRoute] int commentId)
        {
            if (CommentSvc.Delete(commentId))
                return StatusCode(204);
            return StatusCode(500);
        }

        [HttpPut("edit/{commentId}")]
        public IActionResult Put([FromBody] CommentReq req, [FromRoute] int commentId)
        {
            if (HttpContext.Session.GetString("currentUser") != null)
            {
                Comment res = CommentSvc.Update(commentId, req);
                if (res != null)
                    return Ok(res);
                else
                    return StatusCode(500);
            }

            return StatusCode(500);
        }
    }
}
