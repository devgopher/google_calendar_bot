namespace CalendarBot.Settings;

public class ReminderSettings
{
    public static string Section => "ReminderSettings";
    public required string DbConnection { get; set; }

    public required int CheckCalendarEventsIntervalSec { get; set; } = 60;
    
    public required int CheckReminderIntervalSec { get; set; } = 15;
}