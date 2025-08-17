using System.ComponentModel.DataAnnotations;

namespace CalendarBot.Dal.Database.Entities;

/// <summary>
/// Represents a reminder for an event.
/// </summary>
public class Reminder
{
    /// <summary>
    /// Gets or sets the unique identifier for the reminder.
    /// This identifier is used to uniquely identify each reminder instance.
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the associated event.
    /// This identifier links the reminder to a specific event.
    /// </summary>
    [MaxLength(1024)]
    public required string EventId { get; set; }
    
    /// <summary>
    /// Navigation property for the associated event.
    /// This property allows access to the event details related to the reminder.
    /// </summary>
    public virtual Event? Event { get; set; }
    
    /// <summary>
    /// Gets or sets the time span before the event when the reminder should trigger.
    /// This value indicates how long before the event the reminder will be activated.
    /// + 15 sec for Google API
    /// </summary>
    public TimeSpan TimeBefore
    {
        get => _timeBefore;
        set => _timeBefore = value.Add(TimeSpan.FromSeconds(15));
    }

    /// <summary>
    /// Was a reminder already sent to a chat?
    /// </summary>
    public bool IsSent { get; set; }

    private TimeSpan _timeBefore;
}