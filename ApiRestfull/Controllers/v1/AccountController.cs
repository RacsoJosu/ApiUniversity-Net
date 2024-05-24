using ApiRestfull.DataAcces;
using ApiRestfull.Helpers;
using ApiRestfull.Models;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace ApiRestfull.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSetting __jwtSettings;
        private readonly UniversityContext _context;

        public AccountController(JwtSetting jwtSetting, UniversityContext context)
        {
            __jwtSettings = jwtSetting;
            _context = context;

        }

        private IEnumerable<User> Logins = new List<User>()
        {
            new User()
            {
                Id = 1,
                Email = "correo@correo.com",
                Name = "Admin",
                Password= "Admin",

            },
            new User()
            {
                Id = 2,
                Email= "correo2@correo.com",
                Name="User",
                Password="pepe"

            }

        };

        [MapToApiVersion("1.0")]
        [HttpPost]
        public IActionResult GetToken(UserLogin userLogin)
        {
            try
            {
                var token = new UserToken();
                var searchUser = (from user in _context.Users
                                  where user.Name == userLogin.UserName && user.Password == userLogin.Password
                                  select user).FirstOrDefault();



                if (searchUser != null)
                {

                    token = JwtHelper.GenerateToken(new UserToken()
                    {
                        UserName = searchUser.Name,
                        EmailId = searchUser.Email,
                        Id = searchUser.Id,
                        GuidId = Guid.NewGuid(),




                    }, __jwtSettings);
                }
                else
                {
                    return BadRequest("Wrong Passwrod");
                }

                return Ok(token);

            }
            catch (Exception ex)
            {
                throw new Exception("GetToken Error", ex);
            }

        }
        [MapToApiVersion("1.0")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public IActionResult GetUserList()
        {
            return Ok(Logins);
        }



    }
}
