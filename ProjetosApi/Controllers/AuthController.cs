using System.Text.Json;
using Application.Interfaces.OAuth2;
using Microsoft.AspNetCore.Mvc;

namespace ProjetosApi.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IGoogleAuthService _authService;

    public AuthController(IGoogleAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet("google/login")]
    public async Task<IActionResult> GoogleLogin()
    {
        var url = await _authService.GenerateLoginUrlAsync();
        return Ok(new { url });
    }

    [HttpGet("google/callback")]
    public async Task<IActionResult> GoogleCallback([FromQuery] string code)
    {
        var tokenResponse = await _authService.ExchangeCodeForTokenAsync(code);
        return Ok(JsonDocument.Parse(tokenResponse));
    }
}

