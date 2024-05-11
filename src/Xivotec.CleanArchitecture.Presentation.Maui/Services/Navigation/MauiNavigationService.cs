using CommunityToolkit.Mvvm.Messaging;
using Xivotec.CleanArchitecture.Presentation.Core.Messages;
using Xivotec.CleanArchitecture.Presentation.Core.Services.Navigation;

namespace Xivotec.CleanArchitecture.Presentation.Maui.Services.Navigation;

public sealed class MauiNavigationService :
    INavigationService
{
    private const int MinimalPagesForBackNavigation = 2;
    private const int TimeoutForNavigationResponse = 10;

    /// <inheritdoc/>
    public async Task NavigateToAsync(string route)
    {
        // return if navigated to self -> no duplicated pages in page stack
        if (route == Shell.Current.CurrentPage.BindingContext.ToString())
        {
            return;
        }

        // navigate back if new page is previous page
        var navigationStack = Shell.Current.Navigation.NavigationStack;
        if (navigationStack.Count > MinimalPagesForBackNavigation)
        {
            var previousPage = navigationStack[navigationStack.Count - MinimalPagesForBackNavigation];
            var previousRoute = previousPage.BindingContext.GetType().Name;

            if (route == previousRoute)
            {
                await NavigateBackAsync();
                return;
            }
        }
        await Shell.Current.GoToAsync(route, false);
    }

    /// <inheritdoc/>
    public async Task NavigateToAsync(string route, object data)
    {
        var navigationMessage = new NavigationMessageDto
        {
            Route = route,
            Value = data
        };
        await NavigateToAsync(navigationMessage.Route);
        await TransmitMessage(navigationMessage);
    }

    /// <inheritdoc/>
    public async Task NavigateToBaseAsync(string route)
    {
        await NavigateToAsync(route);
        await ReturnToRootAsync();
    }

    /// <inheritdoc/>
    public async Task NavigateBackAsync()
        => await Shell.Current.GoToAsync("..", false);

    private async Task ReturnToRootAsync()
    {
        var stack = Shell.Current.Navigation.NavigationStack;

        // remove all but root page and new base page
        while (stack.Count > 2)
        {
            Shell.Current.Navigation.RemovePage(stack[1]);
        }

        await Task.CompletedTask;
    }

    private async Task TransmitMessage(NavigationMessageDto message)
    {

        // send content to new page
        var acknowledgementTask = new TaskCompletionSource<bool>();

        // configure response handler
        WeakReferenceMessenger.Default.Register<NavigatedToMessage>(this, (_, m) =>
        {
            if (m.Value.Equals(message.Route))
            {
                acknowledgementTask.TrySetResult(true);
            }
            else
            {
                throw new NavigationResponseException("Received NavigatedTo response which is not matching any known navigation");
            }
        });

        WeakReferenceMessenger.Default.Send(new NavigateToMessage(message));

        // wait for acknowledgement message
        var timeoutForNavigation = Task.Delay(TimeSpan.FromSeconds(TimeoutForNavigationResponse));
        var completedTask = await Task.WhenAny(acknowledgementTask.Task, timeoutForNavigation);

        if (completedTask != acknowledgementTask.Task)
        {
            throw new NavigationResponseException("Navigation timed out while waiting for NavigatedTo response");
        }

        WeakReferenceMessenger.Default.Unregister<NavigatedToMessage>(this);
    }
}
