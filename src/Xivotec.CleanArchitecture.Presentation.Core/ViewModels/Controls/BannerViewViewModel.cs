using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Timers;
using Xivotec.CleanArchitecture.Application.Common.Persistence;
using Xivotec.CleanArchitecture.Application.Messages;
using Xivotec.CleanArchitecture.Application.Services.Time;
using Xivotec.CleanArchitecture.Infrastructure.Messages.Error;
using Xivotec.CleanArchitecture.Presentation.Core.Services.Navigation;

namespace Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Controls;

public partial class BannerViewViewModel : ViewModelBase,
    IRecipient<ErrorMessage>,
    IRecipient<ErrorReceivedMessage>,
    IRecipient<NotificationMessage>
{
    [ObservableProperty]
    private string _currentDate = string.Empty;

    [ObservableProperty]
    private string _currentTime = string.Empty;

    [ObservableProperty]
    private string _errorEventMessage = string.Empty;

    [ObservableProperty]
    private string _notificationMessageText = string.Empty;

    private DateTimeOffset _time = DateTimeOffset.UtcNow;

    private System.Timers.Timer _clockTimer = new();
    private System.Timers.Timer _notificationTimer = new();

    private const int ClockUpdateInterval = 1000;
    private const int NotificationTimeout = 10000;

    private readonly ISystemClockService _systemClock;
    private readonly IMediator _mediator;

    public BannerViewViewModel(
        INavigationService navigation,
        ILogger<BannerViewViewModel> logger,
        ISystemClockService systemClock,
        IMediator mediator)
        : base(navigation, logger)
    {
        _systemClock = systemClock;
        _mediator = mediator;

        SetDateTimeTimer();
        CheckDatabaseAvailability();
    }

    public void Receive(ErrorMessage message)
        => ErrorEventMessage = message.Value;

    public void Receive(ErrorReceivedMessage message)
        => ErrorEventMessage = message.Value;

    public void Receive(NotificationMessage message)
    {
        NotificationMessageText = message.Value.Title;
        SetNotificationTimeout();
    }

    private void SetDateTimeTimer()
    {
        _clockTimer = new(ClockUpdateInterval);
        _clockTimer.Elapsed += ClockTimerElapsed;
        _clockTimer.AutoReset = true;
        _clockTimer.Enabled = true;
    }

    private void SetNotificationTimeout()
    {
        _notificationTimer = new(NotificationTimeout);
        _notificationTimer.Elapsed += NotificationTimerFinished;
        _notificationTimer.AutoReset = false;
        _notificationTimer.Enabled = true;
    }

    private void ClockTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        _time = _systemClock.GetCurrentDate();
        CurrentDate = _time.ToString("dd MMMM yyyy");
        CurrentTime = _time.ToString("HH:mm");
    }
    private void NotificationTimerFinished(object? sender, ElapsedEventArgs e)
    {
        NotificationMessageText = "";
    }

    private void CheckDatabaseAvailability() => _mediator.Send(new DatabaseAvailabilityCheckCommand());
}
