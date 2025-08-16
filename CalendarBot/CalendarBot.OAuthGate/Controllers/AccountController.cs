using CalendarBot.OAuthGate.Settings;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CalendarBot.OAuthGate.Controllers;


[Route("api/[controller]")]
public class AccountController : Controller
{
    private readonly IOptionsSnapshot<GoogleAuthSettings> _settings;
    
    public AccountController(IOptionsSnapshot<GoogleAuthSettings> settings)
    {
        _settings = settings;
    }

    [HttpGet("[action]")]
    public async Task<OAuthResponse> GetAuthorizationCode(CancellationToken token) =>
        await "https://accounts.google.com/o/oauth2/auth"
            .SetQueryParams(new
            {
                client_id = _settings.Value.ClientId,
                redirect_uri = _settings.Value.RedirectUri, // GetAccessToken
                response_type = "token",
                scope = "calendar.readonly",
                state = Convert.ToBase64String(Guid.NewGuid().ToByteArray())
            }).GetJsonAsync<OAuthResponse>(cancellationToken: token);
    
    [HttpGet("[action]")]
    public async Task<string> GetAccessToken(TokenRequest request, CancellationToken token)
    {
        var tokenResponse = await "https://oauth2.googleapis.com/token"
            .PostUrlEncodedAsync(new
            {
                code = request.Code,
                client_id = _settings.Value.ClientId,
                redirect_uri = _settings.Value.RedirectUri,
                client_secret = _settings.Value.ClientSecret,
                grant_type = "authorization_code"
            }, cancellationToken: token);

        return await tokenResponse.GetStringAsync(); // Возвращает ответ в формате JSON
    }
}

public class OAuthResponse
{
    public required string Code { get; set; }
    public required string State { get; set; }
}

public class TokenRequest
{
    public required string Code { get; set; }
}