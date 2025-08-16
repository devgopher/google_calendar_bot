namespace CalendarBot.OAuthGate.Controllers;

public class OAuthResponse
{
    public required string Code { get; set; }
    public required string State { get; set; }
}