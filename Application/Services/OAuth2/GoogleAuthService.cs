using Application.Interfaces.OAuth2;
using Microsoft.Extensions.Configuration;
using System.Web;

namespace Application.Services.OAuth2;
public class GoogleAuthService : IGoogleAuthService
{
    private readonly IConfiguration _config;
    private readonly HttpClient _http;

    public GoogleAuthService(IConfiguration config, HttpClient http)
    {
        _config = config;
        _http = http;
    }

    public Task<string> GenerateLoginUrlAsync()
    {
        var clientId = _config["GoogleOAuth:ClientId"];
        var redirectUri = _config["GoogleOAuth:RedirectUri"];
        var scope = HttpUtility.UrlEncode("openid email profile");

        var url = $"https://accounts.google.com/o/oauth2/v2/auth?" +
                  $"client_id={clientId}&redirect_uri={redirectUri}&" +
                  $"response_type=code&scope={scope}&access_type=offline";

        return Task.FromResult(url);
    }

    public async Task<string> ExchangeCodeForTokenAsync(string code)
    {
        var values = new Dictionary<string, string>
        {
            { "code", code },
            { "client_id", _config["GoogleOAuth:ClientId"] },
            { "client_secret", _config["GoogleOAuth:ClientSecret"] },
            { "redirect_uri", _config["GoogleOAuth:RedirectUri"] },
            { "grant_type", "authorization_code" }
        };

        var content = new FormUrlEncodedContent(values);

        var response = await _http.PostAsync("https://oauth2.googleapis.com/token", content);

        var body = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception("Erro ao obter token: " + body);

        return body;
    }
}
