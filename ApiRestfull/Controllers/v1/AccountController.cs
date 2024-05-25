using ApiRestfull.DataAcces;
using ApiRestfull.DTO;
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

      
        [MapToApiVersion("1.0")]
        [HttpPost]
        public IActionResult GetToken(UserLogin userLogin)
        {
            try
            {
                var token = new UserToken();
                var searchUser = (from user in _context.Users
                                  where user.Email == userLogin.Email
                                  select user).FirstOrDefault();




                if (searchUser != null && HandlerPassword.verifyPassword(userLogin.Password,searchUser.Password))
                {

                    token = JwtHelper.GenerateToken(new UserToken()
                    {
                        Role = searchUser.Role,
                        UserName = searchUser.FirstName + " " + searchUser.LastName,
                        EmailId = searchUser.Email,
                        Id = searchUser.Id,
                        GuidId = Guid.NewGuid(),


                        s

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
            var usersList = from user in _context.Users select user;
            return Ok(usersList);
        }

        [MapToApiVersion("1.0")]
        [HttpPost]

        public async Task<ActionResult<User>> Signup(SignupDTO data)
        {

            if (_context.Users.Any(user => user.Email == data.Email))
            {
                return BadRequest("El email ya existe");
                
            }

            var user = new User
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                Password = HandlerPassword.HashPassword(data.Password),
                Email = data.Email

            };


            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }






    }
}
