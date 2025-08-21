using System.Reflection;
using Botticelli.Client.Analytics;
using Botticelli.Controls.Parsers;
using Botticelli.Framework.Commands.Processors;
using Botticelli.Framework.Commands.Validators;
using Botticelli.Framework.SendOptions;
using Botticelli.Shared.API.Client.Requests;
using Botticelli.Shared.ValueObjects;
using FluentValidation;

namespace CalendarBot.Commands.Processors;

public class InfoCommandProcessor<TReplyMarkup> : CommandProcessor<InfoCommand> where TReplyMarkup : class
{
    private readonly SendOptionsBuilder<TReplyMarkup>? _options;

    public InfoCommandProcessor(ILogger<InfoCommandProcessor<TReplyMarkup>> logger,
        ICommandValidator<InfoCommand> commandValidator,
        ILayoutSupplier<TReplyMarkup> layoutSupplier,
        ILayoutParser layoutParser,
        IValidator<Message> messageValidator)
        : base(logger,
            commandValidator,
            messageValidator)
    {
        var responseMarkup = Init(layoutSupplier, layoutParser);

        _options = SendOptionsBuilder<TReplyMarkup>.CreateBuilder(responseMarkup);
    }

    public InfoCommandProcessor(ILogger<InfoCommandProcessor<TReplyMarkup>> logger,
        ICommandValidator<InfoCommand> commandValidator,
        ILayoutSupplier<TReplyMarkup> layoutSupplier,
        ILayoutParser layoutParser,
        IValidator<Message> messageValidator,
        MetricsProcessor? metricsProcessor)
        : base(logger,
            commandValidator,
            messageValidator,
            metricsProcessor)
    {
        var responseMarkup = Init(layoutSupplier, layoutParser);

        _options = SendOptionsBuilder<TReplyMarkup>.CreateBuilder(responseMarkup);
    }

    protected override Task InnerProcessContact(Message message, CancellationToken token) => Task.CompletedTask;

    protected override Task InnerProcessPoll(Message message, CancellationToken token) => Task.CompletedTask;

    protected override Task InnerProcessLocation(Message message, CancellationToken token) => Task.CompletedTask;

    protected override async Task InnerProcess(Message message, CancellationToken token)
    {
        var greetingMessageRequest = new SendMessageRequest
        {
            Message = new Message
            {
                Uid = Guid.NewGuid().ToString(),
                ChatIds = message.ChatIds,
                Body =
                    "The Google Calendar Reminder Bot sends timely notifications for your upcoming events directly from Google Calendar." +
                    " It allows users to customize reminder settings, ensuring they receive alerts at their preferred times. With real-time notifications," +
                    " you’ll never miss important appointments or meetings. The bot provides detailed event information, " +
                    "including titles, times, and locations. \n" +
                    "Stay organized and enhance your productivity with this essential reminder tool!!"
            }
        };

        await SendMessage(greetingMessageRequest, _options, token);
    }

    private static TReplyMarkup Init(ILayoutSupplier<TReplyMarkup> layoutSupplier, ILayoutParser layoutParser)
    {
        var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
        var responseLayout = layoutParser.ParseFromFile(Path.Combine(location, "start_layout.json"));
        var responseMarkup = layoutSupplier.GetMarkup(responseLayout);

        return responseMarkup;
    }
}