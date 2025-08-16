using System.ComponentModel.DataAnnotations;

namespace CalendarBot.Dal.Dal.Entities;

/// <summary>
/// Represents a Google Calendar event.
/// </summary>
public class Event
{
    /// <summary>
    /// Gets or sets the unique identifier for the event.
    /// This property is required.
    /// </summary>
    [Key]
    [MaxLength(1024)]
    public required string Id { get; set; } 

    /// <summary>
    /// Gets or sets the title of the event.
    /// This property is required.
    /// </summary>
    [MaxLength(2048)]
    public string? Summary { get; set; } 

    /// <summary>
    /// Gets or sets the description of the event.
    /// This property is required.
    /// </summary>
    [MaxLength(100000)]
    public required string Description { get; set; }

    /// <summary>
    /// Gets or sets the start date and time of the event.
    /// This property is required.
    /// </summary>
    public DateTime Start { get; set; } 

    /// <summary>
    /// Gets or sets the end date and time of the event.
    /// This property is required.
    /// </summary>
    public DateTime End { get; set; } 

    /// <summary>
    /// Gets or sets the location where the event will take place.
    /// This property is required.
    /// </summary>
    [MaxLength(2048)]
    public string? Location { get; set; }  

    /// <summary>
    /// Gets or sets the list of attendees for the event.
    /// This property is required.
    /// </summary>
    public string[]? Attendees { get; set; } = []; 
}