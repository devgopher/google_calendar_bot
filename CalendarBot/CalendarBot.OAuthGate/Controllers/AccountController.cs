using CalendarBot.Dal.Database.Entities;
using CalendarBot.Dal.Database.Repositories;
using CalendarBot.OAuthGate.Settings;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CalendarBot.OAuthGate.Controllers;

[Route("api/[controller]")]
public class AccountController : Controller
{
    private readonly IAuth _auth;
    private readonly IOptionsSnapshot<GoogleAuthSettings> _settings;

    public AccountController(IOptionsSnapshot<GoogleAuthSettings> settings, IAuth auth)
    {
        _settings = settings;
        _auth = auth;
    }

    [HttpGet("[action]")]
    public Task<string> Authorize(string chatId)
    {
        return Task.FromResult("https://accounts.google.com/o/oauth2/auth"
            .SetQueryParams(new
            {
                client_id = _settings.Value.ClientId,
                redirect_uri = _settings.Value.RedirectUri, // GetAccessToken
                response_type = "token",
                scope = "calendar.readonly",
                state = $"{chatId}_{Convert.ToBase64String(Guid.NewGuid().ToByteArray())}"
            }).ToString());
    }

    [HttpGet("[action]")]
    public async Task<bool> CatchRedirection(TokenRequest request, CancellationToken token)
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

            var oauthToken = await tokenResponse.GetStringAsync();
            var chatId = request.State.Substring(0, request.State.IndexOf('_'));
            // Let's write out token to a DB!

            await _auth.AddTokenAsync(new GoogleTokens
            {
                ChatId = chatId,
                Token = oauthToken,
                RefreshToken = null,
                RefreshTokenDtUtc = default,
                AccessTokenDtUtc = DateTime.UtcNow,
                AccessTokenExpirationUtc = default
            });
        
        return true;
    }

    [HttpGet("[action]")]
    public async Task<DateTime> GetExpiration(string oauthToken, CancellationToken token)
    {
        var response = await $"https://www.googleapis.com/oauth2/v1/tokeninfo?access_token={oauthToken}"
            .GetJsonAsync<GoogleTokenInfo>(cancellationToken: token);
        
        return response.ExpirationDateTime;
    }
}