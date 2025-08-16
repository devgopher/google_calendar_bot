using Botticelli.Interfaces;
using CalendarBot.Dal.Database.Repositories;
using CalendarBot.Integrations.Google;
using Polly;

namespace CalendarBot.HostedServices;

/// <summary>
///     Notifies users
/// </summary>
public class NotifySender : IHostedService
{
    private readonly ICalendarEventService _calendarEventService;
    private readonly ICalendarReader _calendarReader;
    private readonly IBot _bot;
    private readonly CancellationTokenSource _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource();
    private readonly CancellationToken _cancellationToken;
    
    
    public NotifySender(ICalendarEventService calendarEventService, ICalendarReader calendarReader, IBot bot)
    {
        _cancellationToken = _cancellationTokenSource.Token;
        _calendarEventService = calendarEventService;
        _calendarReader = calendarReader;
        _bot = bot;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Policy
            .HandleResult<bool>(_ => _cancellationToken.IsCancellationRequested)
            .WaitAndRetryForeverAsync((_, _, _) => TimeSpan.FromMinutes(1), async (_, _, _) =>
            {
                // var events = await _calendarEventService.GetAllUpcomingEventsAsync();
                // foreach (var @event in events) await _calendarWriter.UpsertEventAsync(@event);
            });
        
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _cancellationTokenSource.CancelAsync();
        _cancellationTokenSource.Dispose();
    }
}