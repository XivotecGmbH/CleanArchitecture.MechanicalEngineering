using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Messages;
public class TemperatureDataChangedMessage(string value) : ValueChangedMessage<string>(value)
{
}
