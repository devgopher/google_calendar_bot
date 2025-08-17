using Botticelli.Interfaces;
using Botticelli.Shared.API.Client.Requests;
using Botticelli.Shared.ValueObjects;
using CalendarBot.Dal.Database.Entities;
using CalendarBot.Dal.Database.Repositories;
using Polly;

namespace CalendarBot.HostedServices;

/// <summary>
///     Notifies users
/// </summary>
public class NotifySender : IHostedService
{
    private readonly ICalendarReader _calendarReader;
    private readonly ICalendarWriter _calendarWriter;
    private readonly IBot _bot;
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly CancellationToken _cancellationToken;
    
    
    public NotifySender(ICalendarReader calendarReader, IBot bot, ICalendarWriter calendarWriter)
    {
        _cancellationToken = _cancellationTokenSource.Token;
        _calendarReader = calendarReader;
        _bot = bot;
        _calendarWriter = calendarWriter;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Policy
            .HandleResult<bool>(_ => _cancellationToken.IsCancellationRequested)
            .WaitAndRetryForeverAsync((_, _, _) => TimeSpan.FromMinutes(1), async (_, _, _) =>
            {
                var utcNow = DateTime.UtcNow;
                var currentReminders = (await _calendarReader.GetAllRemindersAsync()).Where(rem =>
                    rem?.Event != null && rem.Event.Start.ToUniversalTime() - utcNow <= rem.TimeBefore && !rem.IsSent);

                foreach (var reminder in currentReminders)
                {
                    if (reminder?.Event == null)
                        continue;
                    
                    var request = new SendMessageRequest
                    {
                        Message = new Message
                        {
                            Type = Message.MessageType.Messaging,
                            Uid = Guid.NewGuid().ToString(),
                            ChatIds = [reminder.Event.ChatId],
                            Subject = reminder.Event.Summary,
                            Body = reminder.Event.Description
                        }
                    };
                    
                    await _bot.SendMessageAsync(request, cancellationToken);
                    
                    reminder.IsSent = true;
                    await _calendarWriter.UpdateEventAsync(reminder.Event);
                }
            });
        
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _cancellationTokenSource.CancelAsync();
        _cancellationTokenSource.Dispose();
    }
}