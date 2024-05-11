using Xivotec.CleanArchitecture.Domain.Common;
using Xivotec.CleanArchitecture.Domain.NotificationAggregate.Enums;

namespace Xivotec.CleanArchitecture.Domain.NotificationAggregate.Entities;

public class Notification : Entity
{
    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public NotificationType Type { get; set; }

    public bool Acknowledged { get; set; } = false;

    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
}