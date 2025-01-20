using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arguments.Arguments.Token;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Auth
{
    public interface IAuthService
    {
        string GenerateJwtToken(UserData data, HttpContext _httpContext, string sub);
    }
}