using CommunityToolkit.Mvvm.Messaging;
using Xivotec.CleanArchitecture.Application.Common.Persistence;
using Xivotec.CleanArchitecture.Application.Messages;
using Xivotec.CleanArchitecture.Domain.FeatherDeviceAggregate.Enums;
using Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Interface;
using Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Sdk;
using Xivotec.CleanArchitecture.Infrastructure.Messages.Data;
using Xivotec.CleanArchitecture.Infrastructure.Messages.Notification;

namespace Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Facade;

public class FeatherDeviceFacade : RepositoryItem, IFeatherDeviceFacade
{
    private readonly IFeatherSdk _featherSdk;

    public FeatherDeviceFacade(IFeatherSdk featherSdk)
    {
        _featherSdk = featherSdk;
    }

    public Task InitialiseAsync(string port)
    {
        _featherSdk.Initialize(port);

        _featherSdk.SensorDataReceived += OnDeviceStreamDataReceived;
        _featherSdk.NotificationReceived += OnNotificationReceived;

        return Task.CompletedTask;
    }

    public Task DeinitializeAsync()
    {
        _featherSdk.SensorDataReceived -= OnDeviceStreamDataReceived;
        _featherSdk.NotificationReceived -= OnNotificationReceived;

        return Task.CompletedTask;
    }

    public async Task ConnectAsync() => await _featherSdk.Connect();

    public async Task DisconnectAsync() => await _featherSdk.Disconnect();

    public async Task StartAsync() => await _featherSdk.Start();

    public async Task StartTemperatureSensorDataStream() => await _featherSdk.StartTemperatureSensorDataStream();

    public async Task StartDistanceSensorDataStream() => await _featherSdk.StartDistanceSensorDataStream();

    public async Task StartLumenSensorDataStream() => await _featherSdk.StartLumenSensorDataStream();



    public async Task StopAsync()
    {
        await _featherSdk.Stop();
        await _featherSdk.StopTemperatureSensorDataStream();
        await _featherSdk.StopDistanceSensorDataStream();
        await _featherSdk.StopLumenSensorDataStream();
    }

    public async Task ContinueAsync() => await _featherSdk.Continue();

    public async Task PauseAsync() => await _featherSdk.Pause();

    public ConnectionState GetConnectionSate()
    {
        return _featherSdk.ConnectionState;
    }

    private void OnDeviceStreamDataReceived(object sender, FeatherSdkStreamData streamData)
    {
        switch (streamData.Id)
        {
            case 1:
                WeakReferenceMessenger.Default.Send(new TemperatureDataReceivedMessage(streamData.Data));
                break;
            case 2:
                WeakReferenceMessenger.Default.Send(new LumenDataReceivedMessage(streamData.Data));
                break;
            case 3:
                WeakReferenceMessenger.Default.Send(new DistanceDataReceivedMessage(streamData.Data));
                break;
        }
    }

    private void OnNotificationReceived(object sender, FeatherSdkNotification notification)
    {
        if (notification.IsCritical)
        {
            WeakReferenceMessenger.Default.Send(new ErrorMessage(notification.Information));
        }
        else
        {
            WeakReferenceMessenger.Default.Send(new NotificationReceivedMessage(notification.Information));
        }
    }
}