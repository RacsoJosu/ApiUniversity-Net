using ApiRestfull.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace ApiRestfull.Helpers
{
    public static class JwtHelper
    {
        public static IEnumerable<Claim> GetClaims(this UserToken userAccount, Guid Id)
        {
            List<Claim> claims = new List<Claim>() {
                new Claim("Id", userAccount.Id.ToString()),
                new Claim(ClaimTypes.Name, userAccount.UserName),
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MM ddd dd yyyy HH:mm:ss tt"))

            };

            if (userAccount.UserName == "Admin")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));

            } else if (userAccount.UserName == "User") {

                claims.Add(new Claim(ClaimTypes.Role, "User"));
                claims.Add(new Claim("User Only", "User 1"));

            }


            return claims;

        }

        public static IEnumerable<Claim> GetClaims( this UserToken userAccount, out Guid Id ){
            Id = Guid.NewGuid();
            return GetClaims(userAccount, Id);
            
        
        }

        public static UserToken GenerateToken(UserToken model, JwtSetting jwtSenttings)
        {
            try
            {
                var userToken = new UserToken();
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model));
                }

                // obteniendo llave secreta

                var key = System.Text.Encoding.ASCII.GetBytes(jwtSenttings.IssueSigningKey);

                Guid Id;

                //expiracion del token 

                DateTime expireTime = DateTime.UtcNow.AddDays(1);

                // validar token 
                userToken.Validity = expireTime.TimeOfDay;

                // GENERAr nuestro token 

                var jwToken = new JwtSecurityToken(

                    issuer: jwtSenttings.ValidIssuer,
                    audience: jwtSenttings.ValidAudience,
                    claims: GetClaims(model, out Id),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(expireTime).DateTime,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256
                        ));


                userToken.Token = new JwtSecurityTokenHandler().WriteToken(jwToken);

                userToken.UserName = model.UserName;
                userToken.Id = model.Id;
                userToken.GuidId = Id;

                return userToken;



            }
            catch (Exception e) {
                throw new Exception("Error Generation the JWT", e);
                    
            }
        }


    }
}
