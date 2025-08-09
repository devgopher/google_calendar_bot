using System.ComponentModel.DataAnnotations;

namespace CalendarBot.Dal.Entities;

/// <summary>
/// Google OAuth 2.0 tokens
/// </summary>
public class GoogleTokens
{
    /// <summary>
    /// Gets or sets the unique identifier for the chat session.
    /// This identifier is used to associate the tokens with a specific chat.
    /// </summary>
    [MaxLength(128)]
    [Key]
    public required string ChatId { get; set; } 

    /// <summary>
    /// Gets or sets the access token used for authenticating API requests.
    /// This token is required for accessing Google services on behalf of the user.
    /// </summary>
    [MaxLength(1024)]
    public required string Token { get; set; }
    
    /// <summary>
    /// Gets or sets the refresh token used to obtain a new access token.
    /// This token is necessary for maintaining access without requiring user re-authentication.
    /// </summary>
    [MaxLength(1024)]
    public required string RefreshToken { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time when the refresh token was issued.
    /// This information is important for managing token expiration and renewal.
    /// </summary>
    public required DateTime RefreshTokenDtUtc { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time when the access token was issued.
    /// This information is important for managing token expiration and renewal.
    /// </summary>
    public required DateTime AccessTokenDtUtc { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time when the access token expires.
    /// This information is important for managing token expiration and renewal.
    /// </summary>
    public required DateTime AccessTokenExpirationUtc { get; set; }
}