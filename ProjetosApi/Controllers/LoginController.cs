using Application.Interfaces.Auth;
using Arguments.Arguments.OAuth2;
using Arguments.Entities.Token;
using BaseLibrary.Controllers;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("google")]
        public async Task<IActionResult> AuthGoogle([FromBody] InputGoogleToken input)
        {
            if (string.IsNullOrEmpty(input.IdToken))
                return BadRequest("IdToken não enviado.");

            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(input.IdToken, new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { "1057252702144-ck8a5rktbnbggh6u1ehtk1ni3s50dbc6.apps.googleusercontent.com" }
                });
            }
            catch (Exception)
            {
                return Unauthorized("Token Google inválido.");
            }

            if (!payload.EmailVerified)
                return Unauthorized("E-mail não verificado pelo Google.");

            var userData = new UserData
            {
                Email = payload.Email,
                Sub = payload.Subject,
                Identificador = 0, 
                AccessKey = null
            };

            var tokenJwt = _service.GenerateJwtToken(userData, HttpContext, userData.Sub!);
            return Ok(new { Token = tokenJwt });
        }
    }
}
