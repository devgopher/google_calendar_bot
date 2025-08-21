using System.Globalization;
using System.Reflection;
using Botticelli.Client.Analytics;
using Botticelli.Controls.Parsers;
using Botticelli.Framework.Commands.Processors;
using Botticelli.Framework.Commands.Validators;
using Botticelli.Framework.SendOptions;
using Botticelli.Shared.API.Client.Requests;
using Botticelli.Shared.ValueObjects;
using CalendarBot.Integrations.Google;
using FluentValidation;

namespace CalendarBot.Commands.Processors;

public class GetEventsForTodayCommandProcessor<TReplyMarkup>  : CommandProcessor<GetEventsForTodayCommand> 
    where TReplyMarkup : class
{
    private readonly ICalendarEventService _calendarEventService;
    private readonly SendOptionsBuilder<TReplyMarkup>? _options;

    public GetEventsForTodayCommandProcessor(ILogger<GetEventsForTodayCommandProcessor<TReplyMarkup>> logger,
        ICommandValidator<GetEventsForTodayCommand> commandValidator,
        ILayoutSupplier<TReplyMarkup> layoutSupplier,
        ILayoutParser layoutParser,
        ICalendarEventService calendarEventService,
        IValidator<Message> messageValidator)
        : base(logger,
            commandValidator,
            messageValidator)
    {
        _calendarEventService = calendarEventService;
        var responseMarkup = Init(layoutSupplier, layoutParser);

        _options = SendOptionsBuilder<TReplyMarkup>.CreateBuilder(responseMarkup);
    }

    public GetEventsForTodayCommandProcessor(ILogger<GetEventsForTodayCommandProcessor<TReplyMarkup>> logger,
        ICommandValidator<GetEventsForTodayCommand> commandValidator,
        ILayoutSupplier<TReplyMarkup> layoutSupplier,
        ILayoutParser layoutParser,
        ICalendarEventService calendarEventService,
        IValidator<Message> messageValidator,
        MetricsProcessor? metricsProcessor)
        : base(logger,
            commandValidator,
            messageValidator,
            metricsProcessor)
    {
        _calendarEventService = calendarEventService;
        var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
        var responseLayout = layoutParser.ParseFromFile(Path.Combine(location, "start_layout.json"));
        var responseMarkup = layoutSupplier.GetMarkup(responseLayout);

        _options = SendOptionsBuilder<TReplyMarkup>.CreateBuilder(responseMarkup);
    }

    protected override Task InnerProcessContact(Message message, CancellationToken token)
    {
        return Task.CompletedTask;
    }

    protected override Task InnerProcessPoll(Message message, CancellationToken token)
    {
        return Task.CompletedTask;
    }

    protected override Task InnerProcessLocation(Message message, CancellationToken token)
    {
        return Task.CompletedTask;
    }

    protected override async Task InnerProcess(Message message, CancellationToken token)
    {
        var events = (await _calendarEventService.GetAllUpcomingEventsAsync())
            .Where(e => /*e.ChatId == message.ChatIds.FirstOrDefault() &&*/ e.Start >= DateTime.Today &&
                        e.Start < DateTime.Today.AddDays(1))
            .ToArray();

        foreach (var @event in  events)
        {
            var messageRequest = new SendMessageRequest
            {
                Message = new Message
                {
                    Uid = Guid.NewGuid().ToString(),
                    ChatIds = message.ChatIds,
                    Body = $"📆 {@event.Description} \n\n" +
                         $"👉 {@event.Summary} \n" +
                         $"⏰ {@event.Start.ToString("MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture)} \n"
                }
            };

            if (@event.End != default)
                messageRequest.Message.Body += $"🕦 {@event.End.ToString($"MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture)} \n";
            
            if (@event.Location != default)
                messageRequest.Message.Body += $"📍 {@event.Location} \n";
            if (@event.Attendees != default && @event.Attendees.Any())
            {
                messageRequest.Message.Body += $"👥 Attendees: \n";
                foreach (var attendee in @event.Attendees)
                {
                    messageRequest.Message.Body += $"       👤 {attendee} \n";
                }
            }

            await SendMessage(messageRequest, _options, token);   
         
            await Task.Delay(500, token);
        }
    }

    private static TReplyMarkup Init(ILayoutSupplier<TReplyMarkup> layoutSupplier, ILayoutParser layoutParser)
    {
        var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
        var responseLayout = layoutParser.ParseFromFile(Path.Combine(location, "start_layout.json"));
        var responseMarkup = layoutSupplier.GetMarkup(responseLayout);

        return responseMarkup;
    }

}