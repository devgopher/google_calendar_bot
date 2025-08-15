using CalendarBot.OAuthGate.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CalendarBot.OAuthGate.Controllers;

public class AccountController : Controller
{
    private readonly IOptionsSnapshot<GoogleAuthSettings> _settings;

    public AccountController(IOptionsSnapshot<GoogleAuthSettings> settings)
    {
        _settings = settings;
    }

    [HttpGet]
    public async Task<IActionResult> GoogleResponse(string returnUrl = "/")
    {
        var result = await HttpContext.AuthenticateAsync("Cookies");
        var accessToken = await HttpContext.GetTokenAsync("access_token");

        return Redirect(returnUrl); // Перенаправление на исходный URL
    }

    public IActionResult Logout() => SignOut("Cookies", GoogleDefaults.AuthenticationScheme);
}