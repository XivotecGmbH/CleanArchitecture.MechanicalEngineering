using Xivotec.CleanArchitecture.Domain.Common;

namespace Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;

public class ToDoItem : Entity
{
    /// <summary>
    /// Id of the list the Item belongs to
    /// </summary>
    public Guid ListId { get; set; }

    /// <summary>
    /// Title to display
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Note / Description to display
    /// </summary>
    public string Note { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp the ToDoItem is due
    /// </summary>
    public DateTimeOffset Reminder { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Marker whether or not the item is done
    /// </summary>
    public bool Done { get; set; }

    // Needed for list proxy
    public virtual ToDoList List { get; set; }
}
