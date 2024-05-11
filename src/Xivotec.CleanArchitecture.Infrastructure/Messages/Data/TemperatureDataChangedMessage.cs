using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Xivotec.CleanArchitecture.Infrastructure.Messages.Data;
public class TemperatureDataChangedMessage(string value) : ValueChangedMessage<string>(value)
{
}
