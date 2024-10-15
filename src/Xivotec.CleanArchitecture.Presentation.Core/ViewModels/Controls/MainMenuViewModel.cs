using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Xivotec.CleanArchitecture.Presentation.Core.Services.Navigation;
using Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Device;
using Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Notification;
using Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Processes;
using Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Recipe;
using Xivotec.CleanArchitecture.Presentation.Core.ViewModels.TimeSeries;
using Xivotec.CleanArchitecture.Presentation.Core.ViewModels.ToDoList;

namespace Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Controls;

public partial class MainMenuViewModel : ViewModelBase
{
    public MainMenuViewModel(INavigationService navigation,
        ILogger<MainMenuViewModel> logger)
        : base(navigation, logger)
    {
    }

    [RelayCommand]
    public async Task NavigateToHomeAsync()
    => await Navigation.NavigateToBaseAsync(nameof(MainViewModel));

    [RelayCommand]
    public async Task NavigateToRecipeAsync()
    => await Navigation.NavigateToBaseAsync(nameof(RecipeControlViewModel));

    [RelayCommand]
    public async Task NavigateToDeviceAsync()
        => await Navigation.NavigateToBaseAsync(nameof(DeviceViewModel));

    [RelayCommand]
    public async Task NavigateToSettingsAsync()
        => await Navigation.NavigateToBaseAsync(nameof(SettingsViewModel));

    [RelayCommand]
    public async Task NavigateToToDoListAsync()
        => await Navigation.NavigateToBaseAsync(nameof(ToDoListViewModel));

    [RelayCommand]
    public async Task NavigateToProcessesAsync()
        => await Navigation.NavigateToBaseAsync(nameof(ProcessViewModel));

    [RelayCommand]
    public async Task NavigateToNotificationsAsync()
        => await Navigation.NavigateToBaseAsync(nameof(NotificationsViewModel));

    [RelayCommand]
    public async Task NavigateToTimeSeriesDemoAsync()
        => await Navigation.NavigateToBaseAsync(nameof(TimeSeriesDemoViewModel));
}
