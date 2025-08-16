using CalendarBot.Dal.Database.Repositories;
using CalendarBot.OAuthGate.Settings;
using Flurl;
using Microsoft.Extensions.Options;

namespace CalendarBot.Integrations.Auth;

public class Authorizer : IAuthorizer
{
    private readonly IAuth _auth;
    private readonly IOptionsSnapshot<GoogleAuthSettings> _settings;

    public Authorizer(IOptionsSnapshot<GoogleAuthSettings> settings, IAuth auth)
    {
        _settings = settings;
        _auth = auth;
    }

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
}