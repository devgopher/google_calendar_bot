namespace CalendarBot.OAuthGate.Settings;

public class GoogleAuthSettings
{
    public const string Section = "GoogleAuth";
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
}