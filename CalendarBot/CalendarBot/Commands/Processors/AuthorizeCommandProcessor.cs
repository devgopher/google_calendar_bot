using Botticelli.Client.Analytics;
using Botticelli.Controls.BasicControls;
using Botticelli.Controls.Layouts.Inlines;
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

    /// <summary>
    ///     Initializes a new instance of the <see cref="AuthorizeCommandProcessor" /> class.
    /// </summary>
    /// <param name="logger">The logger used for logging information and errors.</param>
    /// <param name="commandValidator">The validator for the authorization command.</param>
    /// <param name="messageValidator">The validator for messages associated with the command.</param>
    /// <param name="authorizer">OAuth support</param>
    /// <param name="layoutSupplier">Layout supplier</param>
    public AuthorizeCommandProcessor(ILogger logger, ICommandValidator<AuthorizeCommand> commandValidator,
        IValidator<Message> messageValidator, IAuthorizer authorizer, ILayoutSupplier<TReplyMarkup> layoutSupplier)
        : base(logger, commandValidator, messageValidator)
    {
        _authorizer = authorizer;
        _layoutSupplier = layoutSupplier;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="AuthorizeCommandProcessor" /> class with metrics processing.
    /// </summary>
    /// <param name="logger">The logger used for logging information and errors.</param>
    /// <param name="commandValidator">The validator for the authorization command.</param>
    /// <param name="messageValidator">The validator for messages associated with the command.</param>
    /// <param name="metricsProcessor">An optional metrics processor for tracking performance metrics.</param>
    /// <param name="authorizer">OAuth support</param>
    /// <param name="layoutSupplier">Layout supplier</param>
    public AuthorizeCommandProcessor(ILogger logger, ICommandValidator<AuthorizeCommand> commandValidator,
        IValidator<Message> messageValidator, MetricsProcessor? metricsProcessor, IAuthorizer authorizer,
        ILayoutSupplier<TReplyMarkup> layoutSupplier)
        : base(logger, commandValidator, messageValidator, metricsProcessor)
    {
        _authorizer = authorizer;
        _layoutSupplier = layoutSupplier;
    }

    /// <summary>
    ///     Processes the authorization command asynchronously.
    /// </summary>
    /// <param name="message">The message containing the authorization details.</param>
    /// <param name="token">A cancellation token to cancel the operation if needed.</param>
    protected override async Task InnerProcess(Message message, CancellationToken token)
    {
        // Implementation for processing the authorization command goes here.
        // This method should handle the logic for authorizing the command based on the provided message.
        var options = await GetOptions(message);

        var messageRequest = new SendMessageRequest
        {
            Message = new Message
            {
                Uid = Guid.NewGuid().ToString(),
                ChatIds = message.ChatIds,
                Body = "Authenticate, please..."
            }
        };

        await SendMessage(messageRequest, options, token);
    }

    private async Task<SendOptionsBuilder<TReplyMarkup>?> GetOptions(Message message)
    {
        var chatId = message.ChatIds.FirstOrDefault();
        if (chatId == null)
            return null;

        var markup = Init(_layoutSupplier, await _authorizer.Authorize(chatId));

        return SendOptionsBuilder<TReplyMarkup>.CreateBuilder(markup);
    }

    private static TReplyMarkup Init(ILayoutSupplier<TReplyMarkup> layoutSupplier, string url)
    {
        var responseLayout = new InlineButtonMenu(1, 1);
        responseLayout.AddControl(new Button
        {
            Content = "Go",
            CallbackData = url
        });

        return layoutSupplier.GetMarkup(responseLayout);
    }
}