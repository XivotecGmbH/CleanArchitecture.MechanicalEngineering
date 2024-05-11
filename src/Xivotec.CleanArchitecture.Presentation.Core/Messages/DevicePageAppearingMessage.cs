using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Xivotec.CleanArchitecture.Presentation.Core.Messages;

public sealed class DevicePageAppearingMessage(bool value) : ValueChangedMessage<bool>(value)
{
}
