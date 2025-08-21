using Botticelli.Controls.Parsers;
using Botticelli.Framework.Commands.Validators;
using Botticelli.Framework.Extensions;
using Botticelli.Framework.Telegram;
using Botticelli.Framework.Telegram.Extensions;
using CalendarBot.Commands;
using CalendarBot.Commands.Processors;
using CalendarBot.Commons.Settings;
using CalendarBot.Dal;
using CalendarBot.Dal.Database;
using CalendarBot.Dal.Database.Repositories;
using CalendarBot.HostedServices;
using CalendarBot.Integrations.Auth;
using CalendarBot.Integrations.Google;
using Google.Apis.Calendar.v3;
using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;
using Telegram.Bot.Types.ReplyMarkups;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<GoogleAuthSettings>(builder.Configuration.GetSection(GoogleAuthSettings.Section));

builder.Services
       .AddTelegramBot(builder.Configuration)
       .Prepare();

builder.Services
       .AddTelegramLayoutsSupport()
       .AddLogging(cfg => cfg.AddNLog())
       .AddSingleton<IAuthorizer, Authorizer>()
       .AddSingleton<IAuth, Auth>()
       .AddSingleton<ILayoutParser, JsonLayoutParser>()
       .AddSingleton<ICalendarWriter, CalendarWriter>()
       .AddSingleton<ICalendarReader, CalendarReader>()
       .AddSingleton<ICalendarEventService, CalendarEventServiceMock>()
       .AddSingleton<CalendarService>();
       
var connection = builder.Configuration.GetSection(ReminderSettings.Section).Get<ReminderSettings>()?.DbConnection;

builder.Services.AddDbContext<CalendarBotDbContext>(opt =>
       opt.UseNpgsql(connection)
              .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking), ServiceLifetime.Singleton);

builder.Services.AddBotCommand<InfoCommand>()
       .AddProcessor<InfoCommandProcessor<ReplyKeyboardMarkup>>()
       .AddValidator<PassValidator<InfoCommand>>();

builder.Services.AddBotCommand<StartCommand>()
       .AddProcessor<StartCommandProcessor<ReplyKeyboardMarkup>>()
       .AddValidator<PassValidator<StartCommand>>();

builder.Services.AddBotCommand<StopCommand>()
       .AddProcessor<StopCommandProcessor<ReplyKeyboardMarkup>>()
       .AddValidator<PassValidator<StopCommand>>();

builder.Services.AddBotCommand<GetEventsForTodayCommand>()
       .AddProcessor<GetEventsForTodayCommandProcessor<ReplyKeyboardMarkup>>()
       .AddValidator<PassValidator<GetEventsForTodayCommand>>();

builder.Services.AddBotCommand<AuthorizeCommand>()
       .AddProcessor<AuthorizeCommandProcessor<InlineKeyboardMarkup>>()
       .AddValidator<PassValidator<AuthorizeCommand>>();

builder.Services.AddHostedService<GoogleUpdater>()
       .AddHostedService<NotifySender>();

var app = builder.Build();

await app.RunAsync();
