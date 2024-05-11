using CommunityToolkit.Mvvm.Messaging.Messages;
namespace Xivotec.CleanArchitecture.Infrastructure.Messages.Data;
public class TemperatureDataReceivedMessage(string value) : ValueChangedMessage<string>(value)
{
}
