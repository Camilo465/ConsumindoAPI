namespace Application.Interfaces.OAuth2;

public interface IGoogleAuthService
{
    Task<string> GenerateLoginUrlAsync();
    Task<string> ExchangeCodeForTokenAsync(string code);
}

