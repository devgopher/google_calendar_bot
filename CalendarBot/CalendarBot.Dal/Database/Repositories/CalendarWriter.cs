using CalendarBot.Dal.Database.Entities;

namespace CalendarBot.Dal.Database.Repositories;

/// <summary>
///     Implementation of the ICalendarWriter interface for writing calendar data.
/// </summary>
public class CalendarWriter : ICalendarWriter
{
    private readonly CalendarBotDbContext _context;

    /// <summary>
    ///     Initializes a new instance of the CalendarWriter class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CalendarWriter(CalendarBotDbContext context)
    {
        _context = context;
    }

    /// <summary>
    ///     Adds a new event asynchronously.
    /// </summary>
    /// <param name="calendarEvent">The event to add.</param>
    public async Task AddEventAsync(Event? calendarEvent)
    {
        await _context.Events.AddAsync(calendarEvent);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    ///     Updates an existing event asynchronously.
    /// </summary>
    /// <param name="calendarEvent">The event to update.</param>
    public async Task UpdateEventAsync(Event? calendarEvent)
    {
        _context.Events.Update(calendarEvent);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    ///     Deletes an event by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the event to delete.</param>
    public async Task DeleteEventAsync(int id)
    {
        var calendarEvent = await _context.Events.FindAsync(id);
        if (calendarEvent != null)
        {
            _context.Events.Remove(calendarEvent);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    ///     Adds a new reminder asynchronously.
    /// </summary>
    /// <param name="reminder">The reminder to add.</param>
    public async Task AddReminderAsync(Reminder? reminder)
    {
        await _context.Reminders.AddAsync(reminder);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    ///     Updates an existing reminder asynchronously.
    /// </summary>
    /// <param name="reminder">The reminder to update.</param>
    public async Task UpdateReminderAsync(Reminder? reminder)
    {
        _context.Reminders.Update(reminder);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteReminderAsync(int id)
    {
        var reminder = await _context.Reminders.FindAsync(id);
        if (reminder != null)
            _context.Reminders.Remove(reminder);
    }
}