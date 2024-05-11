using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Xivotec.CleanArchitecture.Application.Notifications.Dtos;
using Xivotec.CleanArchitecture.Application.Notifications.Service;
using Xivotec.CleanArchitecture.Presentation.Core.Messages;
using Xivotec.CleanArchitecture.Presentation.Core.Services.Navigation;

namespace Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Notification;

public partial class NotificationDetailsViewModel : ViewModelBase
{
    private readonly INotificationService _notificationService;

    [ObservableProperty]
    private NotificationDto _selectedNotification = new();

    [ObservableProperty]
    private string _confirmButtonText = string.Empty;

    [ObservableProperty]
    private bool _isConfirmButtonActive;

    public NotificationDetailsViewModel(
        INavigationService navigation,
        ILogger<NotificationDetailsViewModel> logger,
        INotificationService notificationService)
        : base(navigation, logger)
    {
        _notificationService = notificationService;
    }

    [RelayCommand]
    public async Task NavigateBack()
    {
        await Navigation.NavigateBackAsync();
    }

    [RelayCommand]
    public async Task ConfirmNotification()
    {
        await _notificationService.AcknowledgeNotification(SelectedNotification);
        SelectedNotification.Acknowledged = true;
        SetConfirmButton();
    }

    public override async Task OnNavigatedTo()
    {
        SetConfirmButton();
        await base.OnNavigatedTo();
    }

    protected override Task ApplyNavigationValues(NavigationMessageDto dto)
    {
        SelectedNotification = (NotificationDto)dto.Value;
        SetConfirmButton();
        return Task.CompletedTask;
    }

    private void SetConfirmButton()
    {
        ConfirmButtonText = SelectedNotification.Acknowledged ? "Confirmed" : "Confirm";
        IsConfirmButtonActive = !SelectedNotification.Acknowledged;
    }
}