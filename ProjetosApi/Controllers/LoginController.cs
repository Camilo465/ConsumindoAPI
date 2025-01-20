using Application.Interfaces.Auth;
using Application.Services.Auth;    
using Arguments.Arguments.Token;
using BaseLibrary.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ProjetosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(IAuthService service) : BaseController<IAuthService>(service)
    {
        [HttpPost]
        public IActionResult Auth(InputAuthentication inputAuthentication)
        {
            var validUsers = new List<UserData>
            {
               new UserData { AccessKey = "123456", Email = "teste1@exemplo.com", Identificador = 1 },
                new UserData { AccessKey = "abcdef", Email = "teste2@exemplo.com", Identificador = 2 }
            };
            var user = validUsers.FirstOrDefault(u =>
                   u.AccessKey == inputAuthentication.AccessKey &&
                    u.Email == inputAuthentication.Email);

            if (user == null)
            {
                return Unauthorized("Credenciais inválidas");
            }
            var token = _service.GenerateJwtToken(user, HttpContext, user.Email);

            return Ok(new { Token = token });
        }
    }
}
