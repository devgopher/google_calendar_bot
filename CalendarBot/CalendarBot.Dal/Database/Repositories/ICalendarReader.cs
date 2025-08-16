using CalendarBot.Dal.Database.Entities;

namespace CalendarBot.Dal.Database.Repositories;

/// <summary>
///     Interface for reading calendar-related data.
/// </summary>
public interface ICalendarReader
{
    /// <summary>
    ///     Gets all events asynchronously.
    /// </summary>
    /// <returns>A collection of events.</returns>
    Task<IEnumerable<Event?>> GetAllEventsAsync();

    /// <summary>
    ///     Gets a specific event by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the event.</param>
    /// <returns>The event with the specified ID.</returns>
    Task<Event?> GetEventByIdAsync(int id);

    /// <summary>
    ///     Gets all tokens asynchronously.
    /// </summary>
    /// <returns>A collection of Google tokens.</returns>
    Task<IEnumerable<GoogleTokens?>> GetAllTokensAsync();

    /// <summary>
    ///     Gets a specific token by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the token.</param>
    /// <returns>The token with the specified ID.</returns>
    Task<GoogleTokens?> GetTokenByIdAsync(int id);

    /// <summary>
    ///     Gets all reminders asynchronously.
    /// </summary>
    /// <returns>A collection of reminders.</returns>
    Task<IEnumerable<Reminder?>> GetAllRemindersAsync();

    /// <summary>
    ///     Gets a specific reminder by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the reminder.</param>
    /// <returns>The reminder with the specified ID.</returns>
    Task<Reminder?> GetReminderByIdAsync(int id);
}