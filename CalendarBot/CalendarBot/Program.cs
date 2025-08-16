using Botticelli.Controls.Parsers;
using Botticelli.Framework.Commands.Validators;
using Botticelli.Framework.Extensions;
using Botticelli.Framework.Telegram;
using Botticelli.Framework.Telegram.Extensions;
using CalendarBot.Commands;
using CalendarBot.Commands.Processors;
using CalendarBot.Dal;
using CalendarBot.Dal.Database;
using CalendarBot.Settings;
using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;
using Telegram.Bot.Types.ReplyMarkups;

var builder = WebApplication.CreateBuilder(args);

builder.Services
       .AddTelegramBot(builder.Configuration)
       .Prepare();

builder.Services
       .AddTelegramLayoutsSupport()
       .AddLogging(cfg => cfg.AddNLog())
       .AddSingleton<ILayoutParser, JsonLayoutParser>()
       .AddSingleton<StartCommandProcessor<ReplyKeyboardMarkup>>()
       .AddSingleton<StopCommandProcessor<ReplyKeyboardMarkup>>()
       .AddSingleton<InfoCommandProcessor<ReplyKeyboardMarkup>>()
       .AddScoped<ICommandValidator<InfoCommand>, PassValidator<InfoCommand>>()
       .AddScoped<ICommandValidator<StartCommand>, PassValidator<StartCommand>>()
       .AddScoped<ICommandValidator<StopCommand>, PassValidator<StopCommand>>()
       .AddScoped<ICommandValidator<GetEventsForTodayCommand>, PassValidator<GetEventsForTodayCommand>>();

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
       .AddProcessor<AuthorizeCommandProcessor<ReplyKeyboardMarkup>>()
       .AddValidator<PassValidator<AuthorizeCommand>>();

var app = builder.Build();

await app.RunAsync();
