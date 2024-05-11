using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Xivotec.CleanArchitecture.Infrastructure.Messages.Data;
public class LumenDataChangedMessage(string value) : ValueChangedMessage<string>(value)
{
}
