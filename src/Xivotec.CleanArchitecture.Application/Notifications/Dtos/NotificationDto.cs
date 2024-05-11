namespace Xivotec.CleanArchitecture.Application.Notifications.Dtos;

public class NotificationDto
{
    public Guid Id { get; init; }

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public NotificationTypeDto Type { get; set; }

    public bool Acknowledged { get; set; } = false;

    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
}