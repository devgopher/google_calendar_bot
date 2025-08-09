using Botticelli.Framework.Commands;

namespace CalendarBot.Commands;

/// <summary>
/// Authorizes in Google Calendar
/// </summary>
public class AuthorizeCommand : ICommand
{
    public Guid Id { get; }
}