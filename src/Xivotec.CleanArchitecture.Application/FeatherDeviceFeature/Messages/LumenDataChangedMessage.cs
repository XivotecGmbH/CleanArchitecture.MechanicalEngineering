using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Messages;
public class LumenDataChangedMessage(string value) : ValueChangedMessage<string>(value)
{
}
