namespace CalendarBot.OAuthGate.Controllers;

/// <summary>
/// Represents the response from the Google OAuth 2.0 token info endpoint.
/// </summary>
public class GoogleTokenInfo
{
    /// <summary>
    /// Gets or sets the audience for which the token is intended.
    /// Typically, this is your application's client ID.
    /// </summary>
    public string Aud { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unique identifier for the user associated with the token.
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the scopes that were granted when the token was issued.
    /// Indicates what resources the token can access.
    /// </summary>
    public string Scope { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the number of seconds until the token expires.
    /// </summary>
    public int ExpiresIn { get; set; }

    /// <summary>
    /// Gets or sets the timestamp (in seconds since the epoch) when the token will expire.
    /// </summary>
    public long ExpiresAt { get; set; }
    
    /// <summary>
    /// Gets the expiration date and time as a DateTime object.
    /// </summary>
    public DateTime ExpirationDateTime => DateTimeOffset.FromUnixTimeSeconds(ExpiresAt).UtcDateTime;

    /// <summary>
    /// Gets or sets the timestamp (in seconds since the epoch) when the token was issued.
    /// </summary>
    public long IssuedAt { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user associated with the token.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the user's email address has been verified.
    /// </summary>
    public bool VerifiedEmail { get; set; }

    /// <summary>
    /// Gets or sets the full name of the user.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's first name.
    /// </summary>
    public string GivenName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's last name.
    /// </summary>
    public string FamilyName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL to the user's profile picture.
    /// </summary>
    public string Picture { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's locale (language and region).
    /// </summary>
    public string Locale { get; set; } = string.Empty;
}