using CalendarBot.OAuthGate.Settings;
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
    public async Task<IActionResult> GoogleResponse([FromQuery]string returnUrl)
    {
        var result = await HttpContext.AuthenticateAsync("Cookies");
        var accessToken = await HttpContext.GetTokenAsync("access_token");

        return Redirect(returnUrl); // Перенаправление на исходный URL
    }

    [HttpGet("[action]")]
    public IActionResult Logout() => SignOut("Cookies", GoogleDefaults.AuthenticationScheme);
}