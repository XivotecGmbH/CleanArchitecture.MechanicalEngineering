using CommunityToolkit.Mvvm.Messaging.Messages;
namespace Xivotec.CleanArchitecture.Infrastructure.Messages.Data;

public class LumenDataReceivedMessage(string value) : ValueChangedMessage<string>(value)
{
}
