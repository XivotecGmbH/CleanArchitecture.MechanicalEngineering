using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Xivotec.CleanArchitecture.Infrastructure.Messages.Data;

public class DistanceDataChangedMessage(string value) : ValueChangedMessage<string>(value)
{
}
