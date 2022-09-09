using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SocialMedia03.BLL;
using SocialMedia03.WEB.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Diagnostics;
using SocialMedia03.DAL.Models;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SocialMedia03.Common.Utils;
using SocialMedia03.Common.Res;
using SocialMedia03.Common.Req;

namespace SocialMedia03.WEB.Controllers.API
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class ApiUserController : ControllerBase
    {
        private ReportSvc reportSvc = new ReportSvc();
        private UserSvc userSvc = new UserSvc();

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserReq userModel)
        {
            User user = userSvc.Authenticate(userModel.Email, userModel.Password);

            if (user == null)
                return BadRequest(new { message = "Email or password is incorrect" });

            HttpContext.Session.SetString("currentUser", JsonConvert.SerializeObject(user));
            HttpContext.Session.SetInt32("currentUserId", user.Id);
            HttpContext.Session.SetString("UUID", user.Uuid);
            HttpContext.Session.SetString("currentUserLastname", user.Lastname.Trim());
            HttpContext.Session.SetString("currentUserFirstname", user.Firstname.Trim());
            HttpContext.Session.SetString("currentUserEmail", user.Email.Trim());
            HttpContext.Session.SetString("currentUserAvatar", user.Avatar.Trim());
            HttpContext.Session.SetString("currentUserRole", user.UserRole.Trim());

            return Ok(new
            {
                user.Uuid,
                user.Email,
                user.Firstname,
                user.Lastname,
                user.Avatar,
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserReq req)
        {
            try
            {
                userSvc.RegisterNewUser(req);
                return Ok();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpDelete("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                if (HttpContext.Session.GetString("currentUserRole") == "ROLE_ADMIN")
                { 
                    userSvc.Delete(id);
                    return StatusCode(204);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            return StatusCode(500);
        }

        // api/user/report-user
        [AllowAnonymous]
        [HttpPost("report-user")]
        public void CreateReport([FromBody] ReportReq req)
        {
            int currentUserId = (int)(HttpContext.Session.GetInt32("currentUserId") == null ? 0
            : HttpContext.Session.GetInt32("currentUserId"));

            reportSvc.CreateReport(currentUserId, req);
        }
    }
}
