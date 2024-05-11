using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;

/// <summary>
/// DTO representing a <see cref="ToDoItem"/>.
/// </summary>
public record ToDoItemDto
{
    public Guid Id { get; set; }

    public Guid ListId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Note { get; set; } = string.Empty;

    public DateTimeOffset Reminder { get; set; } = DateTimeOffset.UtcNow;

    public bool Done { get; set; } = false;
}