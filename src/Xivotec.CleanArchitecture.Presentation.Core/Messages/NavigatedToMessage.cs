using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Xivotec.CleanArchitecture.Presentation.Core.Messages;

public class NavigatedToMessage(string value) : ValueChangedMessage<string>(value);