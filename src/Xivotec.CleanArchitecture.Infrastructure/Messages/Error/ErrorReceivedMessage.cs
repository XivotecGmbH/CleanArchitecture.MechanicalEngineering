using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Xivotec.CleanArchitecture.Infrastructure.Messages.Error;
public class ErrorReceivedMessage(string value) : ValueChangedMessage<string>(value)
{
}
