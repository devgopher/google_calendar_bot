using CalendarBot.Dal.Database.Entities;

namespace CalendarBot.Dal.Database.Repositories;

/// <summary>
///     Interface for writing calendar-related data.
/// </summary>
public interface ICalendarWriter
{
    /// <summary>
    ///     Adds a new event asynchronously.
    /// </summary>
    /// <param name="calendarEvent">The event to add.</param>
    Task AddEventAsync(Event? calendarEvent);

    /// <summary>
    ///     Updates an existing event asynchronously.
    /// </summary>
    /// <param name="calendarEvent">The event to update.</param>
    Task UpdateEventAsync(Event? calendarEvent);

    /// <summary>
    ///     Deletes an event by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the event to delete.</param>
    Task DeleteEventAsync(int id);

    /// <summary>
    ///     Adds a new reminder asynchronously.
    /// </summary>
    /// <param name="reminder">The reminder to add.</param>
    Task AddReminderAsync(Reminder? reminder);

    /// <summary>
    ///     Updates an existing reminder asynchronously.
    /// </summary>
    /// <param name="reminder">The reminder to update.</param>
    Task UpdateReminderAsync(Reminder? reminder);
    
    /// <summary>
    ///     Upsert a reminder asynchronously.
    /// </summary>
    /// <param name="reminder">The reminder to update.</param>
    Task UpsertReminderAsync(Reminder? reminder);

    /// <summary>
    ///     Deletes a reminder by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the reminder to delete.</param>
    Task DeleteReminderAsync(int id);

    
        
    /// <summary>
    ///     Upsert an event asynchronously.
    /// </summary>
    /// <param name="@event">The reminder to update.</param>
    Task UpsertEventAsync(Event @event);
}