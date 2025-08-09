namespace CalendarBot.Infrastructure.Google;


/// <summary>
/// Represents a Google OAuth 2.0 client for obtaining access and refresh tokens.
/// </summary>
public class GoogleOAuth(IHttpClientFactory httpClientFactory)
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    /// <summary>
    /// The client ID for the Google OAuth 2.0 application.
    /// </summary>
    private const string ClientId = "Ваш client ID";

    /// <summary>
    /// The client secret for the Google OAuth 2.0 application.
    /// </summary>
    private const string ClientSecret = "Ваш client secret";

    /// <summary>
    /// The redirect URI for the Google OAuth 2.0 application.
    /// </summary>
    private const string RedirectUri = "Ваш redirect URI";

    /// <summary>
    /// The URL for the Google OAuth 2.0 authorization endpoint.
    /// </summary>
    private const string AuthUrl = "https://accounts.google.com/o/oauth2/v2/auth";

    /// <summary>
    /// The URL for the Google OAuth 2.0 token endpoint.
    /// </summary>
    private const string TokenUrl = "https://oauth2.googleapis.com/token";

    /// <summary>
    /// Obtains an access and refresh token pair from the Google OAuth 2.0 service.
    /// </summary>
    /// <param name="code">The authorization code obtained from the Google OAuth 2.0 authorization endpoint.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="TokenResponse"/> object containing the access and refresh tokens, or <c>null</c> if an error occurs.</returns>
    public async Task<TokenResponse?> GetTokensAsync(string code, CancellationToken cancellationToken)
    {
        var httpClient = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Post, TokenUrl)
        {
            Content = new FormUrlEncodedContent([
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", RedirectUri),
                new KeyValuePair<string, string>("client_id", ClientId),
                new KeyValuePair<string, string>("client_secret", ClientSecret)
            ])
        };

        var response = await httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken: cancellationToken);
    }

}