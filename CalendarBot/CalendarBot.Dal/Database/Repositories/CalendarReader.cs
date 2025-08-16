using CalendarBot.Dal.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CalendarBot.Dal.Database.Repositories;

/// <summary>
///     Implementation of the ICalendarReader interface for reading calendar data.
/// </summary>
public class CalendarReader : ICalendarReader
{
    private readonly CalendarBotDbContext _context;

    /// <summary>
    ///     Initializes a new instance of the CalendarReader class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CalendarReader(CalendarBotDbContext context)
    {
        _context = context;
    }

    /// <summary>
    ///     Gets all events asynchronously.
    /// </summary>
    /// <returns>A collection of events.</returns>
    public async Task<IEnumerable<Event?>> GetAllEventsAsync()
    {
        return await _context.Events.ToListAsync();
    }

    /// <summary>
    ///     Gets a specific event by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the event.</param>
    /// <returns>The event with the specified ID.</returns>
    public async Task<Event?> GetEventByIdAsync(int id)
    {
        return await _context.Events.FindAsync(id);
    }

    /// <summary>
    ///     Gets all tokens asynchronously.
    /// </summary>
    /// <returns>A collection of Google tokens.</returns>
    public async Task<IEnumerable<GoogleTokens?>> GetAllTokensAsync()
    {
        return await _context.Tokens.ToListAsync();
    }

    /// <summary>
    ///     Gets a specific token by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the token.</param>
    /// <returns>The token with the specified ID.</returns>
    public async Task<GoogleTokens?> GetTokenByIdAsync(int id)
    {
        return await _context.Tokens.FindAsync(id);
    }

    /// <summary>
    ///     Gets all reminders asynchronously.
    /// </summary>
    /// <returns>A collection of reminders.</returns>
    public async Task<IEnumerable<Reminder?>> GetAllRemindersAsync()
    {
        return await _context.Reminders.ToListAsync();
    }

    /// <summary>
    ///     Gets a specific reminder by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the reminder.</param>
    /// <returns>The reminder with the specified ID.</returns>
    public async Task<Reminder?> GetReminderByIdAsync(int id)
    {
        return await _context.Reminders.FindAsync(id);
    }
}