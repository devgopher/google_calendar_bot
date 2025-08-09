using Botticelli.Framework.Commands;

namespace CalendarBot.Commands;

/// <summary>
/// Sets common reminder params
/// </summary>
public class SetReminderParamsCommand : ICommand
{
    public Guid Id { get; }
}