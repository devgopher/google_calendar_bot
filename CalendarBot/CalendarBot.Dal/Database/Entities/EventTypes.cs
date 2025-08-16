namespace CalendarBot.Dal.Database.Entities;

public enum EventTypes
{
    /// <summary>
    /// Personal events, such as birthdays or gatherings with friends.
    /// </summary>
    Personal,

    /// <summary>
    /// Work-related events associated with professional activities.
    /// </summary>
    Work,

    /// <summary>
    /// Meetings that can be either personal or work-related.
    /// </summary>
    Meeting,

    /// <summary>
    /// Reminders for important events or tasks.
    /// </summary>
    Reminder,

    /// <summary>
    /// Holiday events, such as national holidays.
    /// </summary>
    Holiday,

    /// <summary>
    /// Events that last all day.
    /// </summary>
    AllDayEvent,

    /// <summary>
    /// Virtual meetings, such as those conducted via Zoom or Skype.
    /// </summary>
    VirtualMeeting,

    /// <summary>
    /// Sports events, such as matches or tournaments.
    /// </summary>
    SportsEvent,

    /// <summary>
    /// Cultural events, such as exhibitions or concerts.
    /// </summary>
    CulturalEvent,

    /// <summary>
    /// Educational events, such as seminars or courses.
    /// </summary>
    EducationalEvent
}