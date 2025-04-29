using Application.Interfaces.Auth;
using Application.Entities.Token;
using Arguments.Entities.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.Auth
{
    public class AuthService : IAuthService
    {
        public string GenerateJwtToken(UserData data, HttpContext _httpContext, string sub)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Iss, _httpContext?.Request.Host.Value ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Sub, sub), 
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Identificador", data.Identificador.ToString()) 
            };

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(SecurityKeyJwt.Key));
            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);
            DateTime expires = DateTime.Now.AddHours(24);

            JwtSecurityToken token = new(
            _httpContext?.Request.Host.Value,
            sub,
            claims,
            expires: expires,
            signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
