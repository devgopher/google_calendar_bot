namespace CalendarBot.OAuthGate.Controllers;

public class TokenRequest
{
    public required string State {get;set;}
    public required string Code { get; set; }
}