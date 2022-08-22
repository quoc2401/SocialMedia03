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

namespace SocialMedia03.WEB.Controllers.API
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class ApiUserController : ControllerBase
    {
        private UserSvc userSvc = new UserSvc();
        private IMapper mapper;

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserModel userModel)
        {
            User user = userSvc.Authenticate(userModel.Email, userModel.Password);

            if (user == null)
                return BadRequest(new { message = "Email or password is incorrect" });


            HttpContext.Session.SetInt32("currentUserId", user.Id);
            HttpContext.Session.SetString("currentUserLastname", user.Lastname);
            HttpContext.Session.SetString("currentUserFirstname", user.Firstname);
            HttpContext.Session.SetString("currentUserEmail", user.Email);
            HttpContext.Session.SetString("currentUserAvatar", user.Avatar);

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
        public IActionResult Register([FromBody] UserModel userModel)
        {
            var user = mapper.Map<User>(userModel);
            try
            {
                userSvc.RegisterNewUser(user, userModel.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
