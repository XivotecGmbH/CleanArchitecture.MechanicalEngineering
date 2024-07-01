using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Messages;

public class DistanceDataChangedMessage(string value) : ValueChangedMessage<string>(value)
{
}
