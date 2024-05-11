using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Xivotec.CleanArchitecture.Presentation.Core.Messages;

public sealed class NavigateToMessage(NavigationMessageDto value)
    : ValueChangedMessage<NavigationMessageDto>(value);
