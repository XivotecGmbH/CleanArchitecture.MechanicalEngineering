using CommunityToolkit.Mvvm.Messaging;
using Xivotec.CleanArchitecture.Application.Common.Persistence;
using Xivotec.CleanArchitecture.Application.Messages;
using Xivotec.CleanArchitecture.Infrastructure.Messages.Data;
using Xivotec.CleanArchitecture.Infrastructure.Messages.Notification;
using Xivotec.XivoBlue.MeadowSdk;
using Xivotec.XivoBlue.MeadowSdk.DataTypes;

namespace Xivotec.CleanArchitecture.Infrastructure.Devices.Facade;

public class FeatherDeviceFacade : RepositoryItem, IFeatherDeviceFacade
{
    private MeadowSdk? _meadowSdk;

    public Task InitialiseAsync(string port)
    {
        _meadowSdk = new(port);

        _meadowSdk.Initialize();

        _meadowSdk.SensorDataReceived += OnDeviceStreamDataReceived;
        _meadowSdk.NotificationReceived += OnNotificationReceived;

        return Task.CompletedTask;
    }

    public Task DeinitializeAsync()
    {
        _meadowSdk!.SensorDataReceived -= OnDeviceStreamDataReceived;
        _meadowSdk.NotificationReceived -= OnNotificationReceived;

        return Task.CompletedTask;
    }

    public async Task ConnectAsync() => await _meadowSdk!.Connect();

    public async Task DisconnectAsync() => await _meadowSdk!.Disconnect();

    public async Task StartAsync() => await _meadowSdk!.Start();

    public async Task StartTemperatureSensorDataStream() => await _meadowSdk!.StartTemparatureSensorDataStream();

    public async Task StartDistanceSensorDataStream() => await _meadowSdk!.StartDistanceSensorDataStream();

    public async Task StartLumenSensorDataStream() => await _meadowSdk!.StartLumenSensorDataStream();



    public async Task StopAsync()
    {
        await _meadowSdk!.Stop();
        await _meadowSdk!.StopTemparatureSensorDataStream();
        await _meadowSdk!.StopDistanceSensorDataStream();
        await _meadowSdk!.StopLumenSensorDataStream();
    }

    public async Task ContinueAsync() => await _meadowSdk!.Continue();

    public async Task PauseAsync() => await _meadowSdk!.Pause();

    public ConnectionStates GetConnectionSate() => _meadowSdk!.ConnectionState;

    private void OnDeviceStreamDataReceived(object sender, MeadowSdkStreamData streamData)
    {
        switch (streamData.Id)
        {
            case 1 :
                WeakReferenceMessenger.Default.Send(new TemperatureDataReceivedMessage(streamData.Data));
            break;
            case 2 :
                WeakReferenceMessenger.Default.Send(new LumenDataReceivedMessage(streamData.Data));
            break;
            case 3 :
                WeakReferenceMessenger.Default.Send(new DistanceDataReceivedMessage(streamData.Data));
            break;
        }
    }

    private void OnNotificationReceived(object sender, MeadowSdkNotification notification)
    {
        if (notification.Critical)
        {
            WeakReferenceMessenger.Default.Send(new ErrorMessage(notification.Information));
        }
        else
        {
            WeakReferenceMessenger.Default.Send(new NotificationReceivedMessage(notification.Information));
        }
    }
}