using CommunityToolkit.Mvvm.Messaging;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Messages;
using Xivotec.CleanArchitecture.Application.Messages;
using Xivotec.CleanArchitecture.Application.Notifications.Dtos;
using Xivotec.CleanArchitecture.Presentation.Angular.Server.Services.Signal.Interfaces;

namespace Xivotec.CleanArchitecture.Presentation.Angular.Server.Services.Signal;

public class SignalBackgroundService : IHostedService
{
    private readonly ISignalHub<NotificationMessage> _notificationSignalHub;
    private readonly ISignalHub<ConnectionStateChangedMessage> _connectionStateSignalHub;
    private readonly ISignalHub<DistanceDataChangedMessage> _distanceDataSignalHub;
    private readonly ISignalHub<TemperatureDataChangedMessage> _temperatureDataSignalHub;
    private readonly ISignalHub<LumenDataChangedMessage> _lumenDataSignalHub;

    public SignalBackgroundService(ISignalHub<NotificationMessage> notificationSignalHub,
        ISignalHub<ConnectionStateChangedMessage> connectionStateSignalHub,
        ISignalHub<DistanceDataChangedMessage> distanceDataSignalHub,
        ISignalHub<TemperatureDataChangedMessage> temperatureDataSignalHub,
        ISignalHub<LumenDataChangedMessage> lumenDataSignalHub)
    {
        _notificationSignalHub = notificationSignalHub;
        _connectionStateSignalHub = connectionStateSignalHub;
        _distanceDataSignalHub = distanceDataSignalHub;
        _temperatureDataSignalHub = temperatureDataSignalHub;
        _lumenDataSignalHub = lumenDataSignalHub;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        WeakReferenceMessenger.Default.Register<NotificationMessage>(this, HandleNotificationMessage);
        WeakReferenceMessenger.Default.Register<ConnectionStateChangedMessage>(this, HandleConnectionStateMessage);
        WeakReferenceMessenger.Default.Register<DistanceDataChangedMessage>(this, HandleDistanceDataMessage);
        WeakReferenceMessenger.Default.Register<TemperatureDataChangedMessage>(this, HandleTemperatureDataMessage);
        WeakReferenceMessenger.Default.Register<LumenDataChangedMessage>(this, HandleLumenDataMessage);
        return Task.CompletedTask;
    }

    private void HandleNotificationMessage(object r, NotificationMessage m)
    {
        _notificationSignalHub.SendMessageAsync(m).GetAwaiter().GetResult();
    }

    private void HandleConnectionStateMessage(object r, ConnectionStateChangedMessage m)
    {
        _connectionStateSignalHub.SendMessageAsync(m).GetAwaiter().GetResult();
    }

    private void HandleTemperatureDataMessage(object recipient, TemperatureDataChangedMessage message)
    {
        _temperatureDataSignalHub.SendMessageAsync(message).GetAwaiter().GetResult();
    }

    private void HandleDistanceDataMessage(object recipient, DistanceDataChangedMessage message)
    {
        _distanceDataSignalHub.SendMessageAsync(message).GetAwaiter().GetResult();
    }

    private void HandleLumenDataMessage(object recipient, LumenDataChangedMessage message)
    {
        _lumenDataSignalHub.SendMessageAsync(message).GetAwaiter().GetResult();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        WeakReferenceMessenger.Default.Unregister<NotificationMessage>(this);
        WeakReferenceMessenger.Default.Unregister<ConnectionStateChangedMessage>(this);
        WeakReferenceMessenger.Default.Unregister<DistanceDataChangedMessage>(this);
        WeakReferenceMessenger.Default.Unregister<TemperatureDataChangedMessage>(this);
        WeakReferenceMessenger.Default.Unregister<LumenDataChangedMessage>(this);
        return Task.CompletedTask;
    }
}