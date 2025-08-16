using CalendarBot.Dal.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CalendarBot.Dal.Database.Repositories;

/// <summary>
///     Implementation of the IAuth interface for managing authentication tokens.
/// </summary>
public class Auth : IAuth
{
    private readonly CalendarBotDbContext _context;

    /// <summary>
    ///     Initializes a new instance of the AuthService class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public Auth(CalendarBotDbContext context)
    {
        _context = context;
    }

    /// <summary>
    ///     Adds a new token asynchronously.
    /// </summary>
    /// <param name="token">The token to add.</param>
    public async Task AddTokenAsync(GoogleTokens? token)
    {
        await _context.Tokens.AddAsync(token);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    ///     Updates an existing token asynchronously.
    /// </summary>
    /// <param name="token">The token to update.</param>
    public async Task UpdateTokenAsync(GoogleTokens? token)
    {
        _context.Tokens.Update(token);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    ///     Deletes a token by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the token to delete.</param>
    public async Task DeleteTokenAsync(int id)
    {
        var token = await _context.Tokens.FindAsync(id);
        if (token != null)
        {
            _context.Tokens.Remove(token);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    ///     Gets a token by chat ID asynchronously.
    /// </summary>
    /// <param name="chatId">The chat ID associated with the token.</param>
    /// <returns>The token associated with the specified chat ID.</returns>
    public async Task<GoogleTokens?> GetTokenAsync(string chatId)
    {
        return await _context.Tokens.FirstOrDefaultAsync(t => t.ChatId == chatId);
    }
}