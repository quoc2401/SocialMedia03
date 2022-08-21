using Microsoft.AspNetCore.Mvc;
using SocialMedia03.BLL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialMedia03.WEB.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class ApiCommentController : ControllerBase
    {
        private CommentSvc CommentSvc = new CommentSvc();
        // GET: api/comment?params
        [HttpGet]
        public IActionResult GetCommentByPost(int postId, int? page=0)
        {
            return Ok(CommentSvc.GetCommentsByPost(postId, page));
        }

        // GET api/comment/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/comment/add
        [HttpPost("add")]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/comment/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/comment/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
