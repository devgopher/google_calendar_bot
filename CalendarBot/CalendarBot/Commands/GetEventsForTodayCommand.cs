using Botticelli.Framework.Commands;

namespace CalendarBot.Commands;

/// <summary>
/// Gets events from calendar for today
/// </summary>
public class GetEventsForTodayCommand : ICommand
{
    public Guid Id { get; }
}