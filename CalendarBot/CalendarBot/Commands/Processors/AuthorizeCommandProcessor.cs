using System.Reflection;
using Botticelli.Client.Analytics;
using Botticelli.Controls.Parsers;
using Botticelli.Framework.Commands.Processors;
using Botticelli.Framework.Commands.Validators;
using Botticelli.Framework.SendOptions;
using Botticelli.Shared.API.Client.Requests;
using Botticelli.Shared.ValueObjects;
using CalendarBot.Integrations.Auth;
using FluentValidation;

namespace CalendarBot.Commands.Processors;

/// <summary>
///     Processes authorization commands by extending the base CommandProcessor class.
/// </summary>
public class AuthorizeCommandProcessor<TReplyMarkup> : CommandProcessor<AuthorizeCommand> where TReplyMarkup : class
{
    private readonly IAuthorizer _authorizer;
    private readonly ILayoutSupplier<TReplyMarkup> _layoutSupplier;

    public AuthorizeCommandProcessor(ILogger<AuthorizeCommandProcessor<TReplyMarkup>> logger,
        ICommandValidator<AuthorizeCommand> commandValidator,
        ILayoutSupplier<TReplyMarkup> layoutSupplier,
        ILayoutParser layoutParser,
        IValidator<Message> messageValidator,
        IAuthorizer authorizer)
        : base(logger,
            commandValidator,
            messageValidator)
    {
        _authorizer = authorizer;
        _layoutSupplier = layoutSupplier;
        var responseMarkup = Init(layoutParser);

        SendOptionsBuilder<TReplyMarkup>.CreateBuilder(responseMarkup);
    }

    public AuthorizeCommandProcessor(ILogger<AuthorizeCommandProcessor<TReplyMarkup>> logger,
        ICommandValidator<AuthorizeCommand> commandValidator,
        ILayoutSupplier<TReplyMarkup> layoutSupplier,
        ILayoutParser layoutParser,
        IValidator<Message> messageValidator,
        MetricsProcessor? metricsProcessor,
        IAuthorizer authorizer)
        : base(logger,
            commandValidator,
            messageValidator,
            metricsProcessor)
    {
        _authorizer = authorizer;
        _layoutSupplier = layoutSupplier;
        var responseMarkup = Init(layoutParser);

        SendOptionsBuilder<TReplyMarkup>.CreateBuilder(responseMarkup);
    }

    private TReplyMarkup Init(ILayoutParser layoutParser)
    {
        var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
        var responseLayout = layoutParser.ParseFromFile(Path.Combine(location, "start_layout.json"));
        var responseMarkup = _layoutSupplier.GetMarkup(responseLayout);
        return responseMarkup;
    }

    /// <summary>
    ///     Processes the authorization command asynchronously.
    /// </summary>
    /// <param name="message">The message containing the authorization details.</param>
    /// <param name="token">A cancellation token to cancel the operation if needed.</param>
    protected override async Task InnerProcess(Message message, CancellationToken token)
    {
        var chatId = message.ChatIds.FirstOrDefault();
        if (chatId == null)
            return;

        var messageRequest = new SendMessageRequest
        {
            Message = new Message
            {
                Uid = Guid.NewGuid().ToString(),
                ChatIds = message.ChatIds,
                Body = $"Authenticate, please: {await _authorizer.Authorize(chatId)}"
            }
        };

        await SendMessage(messageRequest, token: token);
    }
}