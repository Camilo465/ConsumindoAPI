using System.IdentityModel.Tokens.Jwt;
using Arguments.Entities.Token;
using Microsoft.Extensions.Caching.Memory;

namespace ProjetosApi.Middlewares
{
    public class HttpRequestVerify
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;

        public HttpRequestVerify(RequestDelegate next, IMemoryCache cache)
        {
            _next = next;
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower() ?? string.Empty;
            var env = context.RequestServices.GetRequiredService<IWebHostEnvironment>();

            
            if ((path == "/" || path.StartsWith("/swagger") || path.StartsWith("/v3/api-docs") || path.StartsWith("/favicon.ico"))
                && env.IsDevelopment())
            {
                await _next(context);
                return;
            }

            
            if (!ExtractTokenMethod(context))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token inválido");
                return;
            }

            
            var email = ExtractTokenClass.Email;
            var rateLimit = int.TryParse(ExtractTokenClass.RateLimit, out var limit) ? limit : 10;
            var cacheKey = $"ratelimit_{email}";
            var currentCount = _cache.Get<int>(cacheKey);

            if (currentCount >= rateLimit)
            {
                context.Response.StatusCode = 429;
                await context.Response.WriteAsync("Rate limit excedido");
                return;
            }

            _cache.Set(cacheKey, currentCount + 1, TimeSpan.FromMinutes(1));

            await _next(context);
        }

        private static bool ExtractTokenMethod(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
                return false;

            var token = authHeader.Split(" ").Last();

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                ExtractTokenClass.Email = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value!;
                ExtractTokenClass.RateLimit = jwtToken.Claims.FirstOrDefault(c => c.Type == "RateLimit")?.Value!;
                ExtractTokenClass.Iss = jwtToken.Claims.FirstOrDefault(c => c.Type == "iss")?.Value!;

                return ExtractTokenClass.Email == "teste1@exemplo.com";
            }
            catch
            {
                return false;
            }
        }
    }
}
