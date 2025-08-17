using CalendarBot.Dal.Database.Repositories;
using CalendarBot.Integrations.Google;
using Polly;

namespace CalendarBot.HostedServices;

/// <summary>
///     Updates inner database from Google
/// </summary>
public class GoogleUpdater : IHostedService
{
    private readonly ICalendarEventService _calendarEventService;
    private readonly ICalendarWriter _calendarWriter;
    private readonly CancellationToken _cancellationToken;
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    
    public GoogleUpdater(ICalendarEventService calendarEventService,
        ICalendarWriter calendarWriter)
    {
        _cancellationToken = _cancellationTokenSource.Token;
        _calendarEventService = calendarEventService;
        _calendarWriter = calendarWriter;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Policy
            .HandleResult<bool>(_ => _cancellationToken.IsCancellationRequested)
            .WaitAndRetryForeverAsync((_, _, _) => TimeSpan.FromMinutes(1), async (_, _, _) =>
            {
                var events = await _calendarEventService.GetAllUpcomingEventsAsync();
                foreach (var @event in events) await _calendarWriter.UpsertEventAsync(@event);
            });
        
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
          await _cancellationTokenSource.CancelAsync();
          _cancellationTokenSource.Dispose();
    }
}