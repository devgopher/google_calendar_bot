namespace CalendarBot.Infrastructure.Google;

/// <summary>
/// Represents a token response from the Google OAuth 2.0 service.
/// </summary>
public class TokenResponse
{
    /// <summary>
    /// The access token obtained from the Google OAuth 2.0 service.
    /// </summary>
    public required string AccessToken { get; set; }

    /// <summary>
    /// The refresh token obtained from the Google OAuth 2.0 service.
    /// </summary>
    public required string RefreshToken { get; set; }

    /// <summary>
    /// The number of seconds until the access token expires.
    /// </summary>
    public int ExpiresIn { get; set; }

    /// <summary>
    /// The type of token obtained from the Google OAuth 2.0 service.
    /// </summary>
    public required string TokenType { get; set; }
}