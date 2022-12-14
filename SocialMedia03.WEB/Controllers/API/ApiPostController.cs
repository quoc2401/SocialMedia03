using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SocialMedia03.BLL;
using SocialMedia03.Common.Req;
using SocialMedia03.Common.Res;
using SocialMedia03.Common.Utils;
using SocialMedia03.DAL;
using SocialMedia03.DAL.Models;
using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.Security.Cryptography;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialMedia03.WEB.Controllers.API
{
    [Route("api/post")]
    [ApiController]
    public class ApiPostController : ControllerBase
    {
        private readonly ILogger _logger;
        private ReportSvc reportSvc = new ReportSvc();

        private PostSvc postSvc = new PostSvc();

        private UserSvc userSvc = new UserSvc();

        // GET: api/post/feeds
        [HttpGet("feeds")]
        public IActionResult GetPosts(int page, string? kw = "")
        {
            return Ok(postSvc.Get(page, kw));
        }

        // GET api/post/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            SingleRes res = new SingleRes();
            res.Data = postSvc.Get(id);
            return Ok(res);
        }

        // POST api/post/add
        [HttpPost("add")]
        public IActionResult Post([FromBody]PostReq req)
        {

            User currentUser = new User();
            if (HttpContext.Session.GetString("currentUser") != null)
            {
                currentUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("currentUser"));
                Post res = postSvc.Create(req, currentUser);
                if (res != null)
                    return Ok(res);

            }

            return StatusCode(500);
        }

        // PUT api/posts/edit/5
        [HttpPut("edit/{postId}")]
        public IActionResult Put([FromBody]PostReq req, [FromRoute]int postId)
        {
            User currentUser = new User();
            if (HttpContext.Session.GetString("currentUser") != null)
            {
                currentUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("currentUser"));
                Post res = postSvc.Update(postId, req, currentUser);
                if (res != null)
                    return Ok(res);
                else 
                    return StatusCode(500);
            }

            return StatusCode(500);
        }

        // DELETE api/post/delete/5
        [HttpDelete("delete/{postId}")]
        public IActionResult Delete([FromRoute]int postId)
        {
            if(postSvc.Delete(postId))
                return StatusCode(204);
            return StatusCode(500);
        }

        [HttpPost("image-upload")]
        public IActionResult UploadImage([FromForm] IFormFile file)
        {
            string secure_url = "";
            JToken uploadResult = CloudinaryUtils.UploadImage(file);
            if (uploadResult["error"] == null)
                secure_url = String.Format("{0}?public_id={1}", uploadResult["secure_url"].ToString(), uploadResult["public_id"].ToString());

            SingleRes res = new SingleRes();
            res.Data = secure_url;
            res.Code = "200";

            return Ok(res);
        }

        
        // api/post/create-report
        [HttpPost("create-report")]
        public void CreateReport([FromBody] ReportReq req)
        {
            int currentUserId = (int)(HttpContext.Session.GetInt32("currentUserId") == null ? 0
            : HttpContext.Session.GetInt32("currentUserId"));

            reportSvc.CreateReport(currentUserId, req);
        }
        
        [HttpGet("find")]
        public IActionResult FindPost([FromQuery]int commentId)
        {
            return Ok(postSvc.FindPostByComment(commentId));
        }


        [HttpGet("hashtag")]
        public IActionResult SearchHashtag([FromQuery] string hashtag, [FromQuery] int page)
        {

            return Ok(postSvc.SearchByHashtag(hashtag, page));
        }


        [Route("user")]
        public IActionResult SearchUser([FromQuery] string? kw, int page, int? limit = 0)
        {
            return Ok(userSvc.SearchByUser(kw, page, (int)limit));
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetUserPosts([FromRoute] int userId,[FromQuery] int page)
        {
            return Ok(postSvc.GetPostByUser(userId, page));
        }
    }
}
