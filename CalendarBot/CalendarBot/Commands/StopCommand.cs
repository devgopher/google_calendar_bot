using Botticelli.Framework.Commands;

namespace CalendarBot.Commands;

/// <summary>
/// Stops a bot and deletes all reminders
/// </summary>
public class StopCommand : ICommand
{
    public Guid Id { get; }
}