namespace CalendarBot.Integrations.Auth;

public interface IAuthorizer
{
    public Task<string> Authorize(string chatId);
}