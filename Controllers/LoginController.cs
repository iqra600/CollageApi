using CollageApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CollageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration configuration;

        public LoginController(IConfiguration config)
        {
            configuration = config;
                
        }
        [AllowAnonymous]

        [HttpPost]
        public ActionResult login(string username="admin",string password="admin123" )
        {
            var login = new login() { username = username };
            if (username== "admin" && password == "admin123")
            {
                //write logic to create token
                //fatching key from appsetting.json file.
                var key =Encoding.ASCII.GetBytes( configuration.GetValue<string>("JWTSecret"));
                var handler = new JwtSecurityTokenHandler() {
               


                };
                var descrptor = new SecurityTokenDescriptor()
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name,username),
                        new Claim(ClaimTypes.Role,"Admin")

                    }),


                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512),
                    //Expires = DateTime.Now.AddHours(4)



                };
              var token=  handler.CreateToken(descrptor);
               login.token = handler.WriteToken(token);
               

                return Ok(login);
            }
            else
            {
                return NotFound();
            }



        }

    }
}
