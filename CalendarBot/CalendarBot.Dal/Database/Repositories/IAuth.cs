using CalendarBot.Dal.Database.Entities;

namespace CalendarBot.Dal.Database.Repositories;

/// <summary>
///     Interface for managing authentication tokens.
/// </summary>
public interface IAuth
{
    /// <summary>
    ///     Adds a new token asynchronously.
    /// </summary>
    /// <param name="token">The token to add.</param>
    Task AddTokenAsync(GoogleTokens? token);

    /// <summary>
    ///     Updates an existing token asynchronously.
    /// </summary>
    /// <param name="token">The token to update.</param>
    Task UpdateTokenAsync(GoogleTokens? token);

    /// <summary>
    ///     Deletes a token by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the token to delete.</param>
    Task DeleteTokenAsync(int id);

    /// <summary>
    ///     Gets a token by chat ID asynchronously.
    /// </summary>
    /// <param name="chatId">The chat ID associated with the token.</param>
    /// <returns>The token associated with the specified chat ID.</returns>
    Task<GoogleTokens?> GetTokenAsync(string chatId);
}