using CommunityToolkit.Mvvm.Messaging.Messages;
using Xivotec.CleanArchitecture.Application.Notifications.Dtos;

namespace Xivotec.CleanArchitecture.Application.Messages;

public class NotificationMessage : ValueChangedMessage<NotificationDto>
{
    public NotificationMessage(NotificationDto value) : base(value)
    {
    }
}