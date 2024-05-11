using CommunityToolkit.Mvvm.Messaging.Messages;
namespace Xivotec.CleanArchitecture.Infrastructure.Messages.Data;
public class DistanceDataReceivedMessage(string value) : ValueChangedMessage<string>(value)
{
}
