using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Event = CalendarBot.Dal.Database.Entities.Event;

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

public class CalendarEventService : ICalendarEventService
{
    private readonly CalendarService _calendarService;

    /// <summary>
    /// Initializes a new instance of the CalendarEventService class.
    /// </summary>
    /// <param name="calendarService">The Google Calendar service instance.</param>
    public CalendarEventService(CalendarService calendarService)
    {
        _calendarService = calendarService;
    }

    /// <summary>
    /// Gets all upcoming events from the user's primary calendar asynchronously.
    /// </summary>
    /// <returns>A collection of events.</returns>
    public async Task<IEnumerable<Event>> GetAllUpcomingEventsAsync()
    {
        var request = _calendarService.Events.List("primary");
        request.TimeMin = DateTime.Now; // Get events starting from now
        request.ShowDeleted = false;
        request.SingleEvents = true;
        request.MaxResults = 10; // Limit the number of results
        request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

        Events events = await request.ExecuteAsync();
        return events.Items;
    }

    /// <summary>
    /// Gets a specific event by its ID asynchronously.
    /// </summary>
    /// <param name="eventId">The ID of the event.</param>
    /// <returns>The event with the specified ID.</returns>
    public async Task<Event> GetEventByIdAsync(string eventId)
    {
        return await _calendarService.Events.Get("primary", eventId).ExecuteAsync();
    }
}