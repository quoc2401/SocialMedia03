using Microsoft.AspNetCore.Mvc;
using SocialMedia03.BLL;
using SocialMedia03.Common.Res;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialMedia03.WEB.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class ApiPostController : ControllerBase
    {
        private PostSvc postSvc = new PostSvc();
        // GET: api/post/feeds
        [HttpGet("feeds")]
        public IActionResult GetPosts([FromQuery(Name = "page")] int page,
                [FromQuery(Name = "kw")] string kw="")
        {
            return Ok(this.postSvc.GetPost(page,kw));
        }

        // GET api/post/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            SingleRes res = new SingleRes();
            res.Data = this.postSvc.Get(id);
            return Ok(res);
        }

        // POST api/posts/add
        [HttpPost("add")]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/posts/edit/5
        [HttpPut("edit/{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/posts/delete/5
        [HttpDelete("delete/{id}")]
        public void Delete(int id)
        {
        }
    }
}
