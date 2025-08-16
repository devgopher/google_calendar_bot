using CalendarBot.Dal.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace CalendarBot.Dal.Dal;

public class CalendarBotDbContext : DbContext
{
    public CalendarBotDbContext(DbContextOptions<CalendarBotDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Event> Events { get; set; }
    
    public virtual DbSet<GoogleTokens> Tokens { get; set; }

    public virtual DbSet<Reminder> Reminders { get; set; }
}
