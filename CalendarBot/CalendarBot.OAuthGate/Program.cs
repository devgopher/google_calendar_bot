using CalendarBot.Commons.Settings;
using CalendarBot.Dal.Database;
using CalendarBot.Dal.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
// Добавление сервисов в контейнер зависимостей
builder.Services.AddControllersWithViews();

// Добавление Swagger
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Google Oath API", Version = "v1" }));

var connection = builder.Configuration.GetSection(GoogleAuthSettings.Section).Get<GoogleAuthSettings>()?.DbConnection;

builder.Services.AddDbContext<CalendarBotDbContext>(opt =>
    opt.UseNpgsql(connection)
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking), ServiceLifetime.Singleton);


builder.Services.Configure<GoogleAuthSettings>(builder.Configuration.GetSection(GoogleAuthSettings.Section));

builder.Services.AddScoped<IAuth, Auth>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Google Oath API");
    });
}

app.UseRouting();
app.MapControllers();

await app.RunAsync();