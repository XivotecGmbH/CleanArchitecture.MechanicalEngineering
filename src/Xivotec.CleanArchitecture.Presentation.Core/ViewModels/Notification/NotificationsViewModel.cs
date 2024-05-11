using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using Xivotec.CleanArchitecture.Application.Notifications.Dtos;
using Xivotec.CleanArchitecture.Application.Notifications.Service;
using Xivotec.CleanArchitecture.Presentation.Core.Services.Navigation;

namespace Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Notification;

public partial class NotificationsViewModel : ViewModelBase
{
    private readonly INotificationService _notificationService;

    [ObservableProperty]
    private ObservableCollection<NotificationDto> _notifications = [];

    public NotificationsViewModel(
        INavigationService navigation,
        ILogger<NotificationsViewModel> logger,
        INotificationService notificationService)
        : base(navigation, logger)
    {
        _notificationService = notificationService;
    }

    [RelayCommand]
    public async Task AcknowledgeTapped(NotificationDto notification)
    {
        var indexOfNotificationToUpdate = Notifications.IndexOf(notification);
        Notifications[indexOfNotificationToUpdate].Acknowledged = true;

        await _notificationService.AcknowledgeNotification(notification);
        SortNotifications();
    }

    [RelayCommand]
    public async Task NotificationTapped(NotificationDto notification)
    {
        await Navigation.NavigateToAsync(nameof(NotificationDetailsViewModel), notification);
    }

    public override async Task PageAppearing()
    {
        var notifications = await _notificationService.GetAllNotifications();
        Notifications = [];
        notifications.ForEach(Notifications.Add);

        SortNotifications();
    }

    private void SortNotifications()
    {
        var confirmedNotifications = Enumerable
            .Where<NotificationDto>(Notifications, notification => notification.Acknowledged)
            .OrderBy(notification => notification.Timestamp);

        var unConfirmedNotifications = Enumerable
            .Where<NotificationDto>(Notifications, notification => !notification.Acknowledged)
            .OrderBy(notification => notification.Timestamp);

        var sortedNotifications = unConfirmedNotifications.Concat(confirmedNotifications);
        Notifications.Clear();
        foreach (var notification in sortedNotifications)
        {
            Notifications.Add(notification);
        }

    }
}