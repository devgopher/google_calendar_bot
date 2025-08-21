using CalendarBot.Dal.Database.Entities;

namespace CalendarBot.Integrations.Google;

public class CalendarEventServiceMock : ICalendarEventService
{
    public Task<IEnumerable<Event>> GetAllUpcomingEventsAsync()
    {
        return Task.FromResult<IEnumerable<Event>>([
            new Event
            {
                Id = Guid.NewGuid()
                    .ToString(),
                ChatId = "1111111",
                EventType = EventTypes.VirtualMeeting,
                Summary = "Meeting with friends",
                Description = "Meeting with Alex, Peter and Susanna",
                Start = DateTime.UtcNow.AddHours(2),
                End = DateTime.UtcNow.AddHours(3),
                Location = "VCS: 405039928",
                Attendees =
                [
                    "Peter <peter@noname.com>", "Susanna <susanna@noname.com>", "Alex <alex@noname.com>"
                ]
            },
            new Event
            {
                Id = Guid.NewGuid()
                    .ToString(),
                ChatId = "1111111",
                EventType = EventTypes.Reminder,
                Summary = "Check my mail",
                Description = "Check my mail in the morning",
                Start = DateTime.Today,
                End = DateTime.MaxValue,
                Location = "Home"
            }
        ]);
    }

    public Task<Event> GetEventByIdAsync(string eventId)
        => Task.FromResult<Event>(new()
        {
            Id = Guid.NewGuid()
                .ToString(),
            ChatId = "1111111",
            EventType = EventTypes.Reminder,
            Summary = "Check my mail",
            Description = "Check my mail",
            Start = DateTime.Today,
            End = DateTime.MaxValue,
            Location = "Home"
        });
}