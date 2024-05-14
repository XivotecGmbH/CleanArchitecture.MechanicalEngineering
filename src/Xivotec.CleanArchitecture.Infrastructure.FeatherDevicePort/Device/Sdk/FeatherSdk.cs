using Xivotec.CleanArchitecture.Application.RecipeFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.FeatherDeviceAggregate.Enums;
using Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Interface;

namespace Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Sdk;

/// <summary>
/// Minimal SDK implementation. Placeholder for actual SDK.
/// </summary>
public class FeatherSdk : IFeatherSdk
{
    public event FeatherSdkStreamDataHandler? SensorDataReceived;

    public event FeatherSdkNotificationHandler? NotificationReceived;

    public ConnectionState ConnectionState { get; set; }

    public Task Initialize(string comPort)
    {
        return Task.CompletedTask;
    }

    public Task Connect()
    {
        return Task.CompletedTask;
    }

    public Task Disconnect()
    {
        return Task.CompletedTask;
    }

    public Task LoadRecipe(XivotecRecipeDto recipe)
    {
        return Task.CompletedTask;
    }

    public Task GetCurrentRecipe()
    {
        return Task.CompletedTask;
    }

    public Task Start()
    {
        return Task.CompletedTask;
    }

    public Task Stop()
    {
        return Task.CompletedTask;
    }

    public Task Pause()
    {
        return Task.CompletedTask;
    }

    public Task Continue()
    {
        return Task.CompletedTask;
    }

    public Task StartDistanceSensorDataStream()
    {
        return Task.CompletedTask;
    }

    public Task StopDistanceSensorDataStream()
    {
        return Task.CompletedTask;
    }

    public Task GetDistanceMeasurement()
    {
        return Task.CompletedTask;
    }

    public Task StartLumenSensorDataStream()
    {
        return Task.CompletedTask;
    }

    public Task StopLumenSensorDataStream()
    {
        return Task.CompletedTask;
    }

    public Task GetLumenMeasurement()
    {
        return Task.CompletedTask;
    }

    public Task StartTemperatureSensorDataStream()
    {
        return Task.CompletedTask;
    }

    public Task StopTemperatureSensorDataStream()
    {
        return Task.CompletedTask;
    }

    public Task GetTemperatureMeasurement()
    {
        return Task.CompletedTask;
    }

    public Task<Dictionary<int, FeatherSdkNotification>> GetNotifications()
    {
        return Task.FromResult(new Dictionary<int, FeatherSdkNotification>());
    }

    public Task SendNotificationAcknowledge(int notificationId)
    {
        return Task.CompletedTask;
    }
}