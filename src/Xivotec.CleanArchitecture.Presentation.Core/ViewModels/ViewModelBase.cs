using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using Xivotec.CleanArchitecture.Presentation.Core.Messages;
using Xivotec.CleanArchitecture.Presentation.Core.Services.Navigation;

namespace Xivotec.CleanArchitecture.Presentation.Core.ViewModels;

public abstract partial class ViewModelBase :
    ObservableObject,
    IRecipient<NavigateToMessage>
{
    protected INavigationService Navigation { get; }
    protected ILogger Logger { get; }

    protected ViewModelBase(INavigationService navigation, ILogger logger)
    {
        Navigation = navigation;
        Logger = logger;
        WeakReferenceMessenger.Default.RegisterAll(this);
    }

    protected virtual async Task Initialize()
        => await Task.CompletedTask;

    /// <summary>
    /// Relay Commands using the MAUI intern ShellContent Navigation Stack for Base Pages.
    /// </summary>

    [RelayCommand]
    public async Task NavigateBackAsync()
        => await Navigation.NavigateBackAsync();

    /// <summary>
    /// Relay Command stub for OnNavigatedTo Event.
    /// </summary>
    [RelayCommand]
    public virtual async Task OnNavigatedTo()
        => await Task.CompletedTask;

    /// <summary>
    /// Relay Command stub for Appearing Event.
    /// </summary>
    [RelayCommand]
    public virtual async Task PageAppearing()
        => await Task.CompletedTask;

    /// <summary>
    /// Relay Command stub for Loaded Event.
    /// </summary>
    [RelayCommand]
    public virtual async Task PageLoaded()
        => await Task.CompletedTask;

    public void Receive(NavigateToMessage message)
    {
        var messageEntry = message.Value;
        if (messageEntry.Route != GetType().Name)
        {
            return;
        }
        WeakReferenceMessenger.Default.Send(new NavigatedToMessage(messageEntry.Route));
        ApplyNavigationValues(messageEntry);
    }

    protected virtual Task ApplyNavigationValues(NavigationMessageDto dto)
    {
        return Task.CompletedTask;
    }
}
