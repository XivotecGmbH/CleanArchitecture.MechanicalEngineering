using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Xivotec.CleanArchitecture.Presentation.Core.Messages;

public class FilePathApprovedMessage(string value) : ValueChangedMessage<string>(value)
{
}
