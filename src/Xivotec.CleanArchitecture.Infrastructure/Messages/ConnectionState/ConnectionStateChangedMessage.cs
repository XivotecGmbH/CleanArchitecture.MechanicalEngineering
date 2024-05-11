using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Xivotec.CleanArchitecture.Infrastructure.Messages.ConnectionState;

public class ConnectionStateChangedMessage(bool value) : ValueChangedMessage<bool>(value)
{
}
