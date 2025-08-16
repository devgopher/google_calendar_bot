using CalendarBot.Dal.Database.Entities;

namespace CalendarBot.Integrations.Google;

public interface ICalendarEventService
{
    /// <summary>
    /// Gets all upcoming events from the user's primary calendar asynchronously.
    /// </summary>
    /// <returns>A collection of events.</returns>
    Task<IEnumerable<Event>> GetAllUpcomingEventsAsync();

    /// <summary>
    /// Gets a specific event by its ID asynchronously.
    /// </summary>
    /// <param name="eventId">The ID of the event.</param>
    /// <returns>The event with the specified ID.</returns>
    Task<Event> GetEventByIdAsync(string eventId);
}