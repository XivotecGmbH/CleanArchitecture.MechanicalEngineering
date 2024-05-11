using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Xivotec.CleanArchitecture.Infrastructure.Messages.Notification;
public class NotificationReceivedMessage(string value) : ValueChangedMessage<string>(value)
{
}
