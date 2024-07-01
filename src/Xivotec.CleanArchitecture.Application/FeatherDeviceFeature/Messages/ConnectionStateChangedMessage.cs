using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Messages;

public class ConnectionStateChangedMessage(bool value) : ValueChangedMessage<bool>(value)
{
}
