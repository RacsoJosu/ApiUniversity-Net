using ApiRestfull.DataAcces;
using ApiRestfull.Helpers;
using ApiRestfull.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace ApiRestfull.Controllers
{
    [Route("api/[controller]/[action]")]
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


        [HttpPost]
        public async Task<IActionResult> GetToken(UserLogin userLogin) 
        {
            try 
            {
                var token = new UserToken();
                var searchUser = await _context.Users.FindAsync(userLogin.UserName);
                var valid = Logins.Any( user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));
                if (valid)
                {
                    var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));
                    token = JwtHelper.GenerateToken(new UserToken()
                    {
                        UserName= user.Name,
                        EmailId= user.Email,
                        Id= user.Id,
                        GuidId= Guid.NewGuid(),


                        

                    }, __jwtSettings );
                }else
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

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles ="Administrator")]
        public IActionResult GetUserList()
        {
            return Ok(Logins);
        }
        

        
    }
}
