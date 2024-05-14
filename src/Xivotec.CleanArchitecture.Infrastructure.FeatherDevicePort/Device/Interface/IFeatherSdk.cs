using Xivotec.CleanArchitecture.Application.RecipeFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.FeatherDeviceAggregate.Enums;
using Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Sdk;

namespace Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Interface;

public interface IFeatherSdk
{
    public event FeatherSdkStreamDataHandler? SensorDataReceived;

    public event FeatherSdkNotificationHandler? NotificationReceived;

    /// <summary>
    /// Represents the current device connection state.
    /// </summary>
    public ConnectionState ConnectionState { get; }

    /// <summary>
    /// Init the device.
    /// </summary>
    /// <param name="comPort">The COM Port the device is connected to.</param>
    /// <returns>The completed Task.</returns>
    public Task Initialize(string comPort);

    /// <summary>
    /// Connect to the device. Required for all device operations.
    /// </summary>
    /// <returns>The completed Task.</returns>
    public Task Connect();

    /// <summary>
    /// Disconnect from the device.
    /// </summary>
    /// <returns>The completed Task.</returns>
    public Task Disconnect();

    /// <summary>
    /// Load a new recipe onto the device. The recipe is applied immediately.
    /// </summary>
    /// <param name="recipe">The new <see cref="XivotecRecipeDto"/>.</param>
    /// <returns>The completed Task.</returns>
    public Task LoadRecipe(XivotecRecipeDto recipe);

    /// <summary>
    /// Get the currently operating recipe from the device.
    /// </summary>
    /// <returns>The completed Task.</returns>
    public Task GetCurrentRecipe();

    /// <summary>
    /// Start a demo operation on the device.
    /// </summary>
    /// <returns>The completed Task.</returns>
    public Task Start();

    /// <summary>
    /// Stop the currently running demo operation on the device.
    /// </summary>
    /// <returns>The completed Task.</returns>
    public Task Stop();

    /// <summary>
    /// Pause the currently running demo operation on the device.
    /// </summary>
    /// <returns>The completed Task.</returns>
    public Task Pause();

    /// <summary>
    /// Continue the currently paused demo operation on the device.
    /// </summary>
    /// <returns></returns>
    public Task Continue();

    /// <summary>
    /// Start the continuous distance measurement data stream.
    /// </summary>
    /// <returns>The completed Task.</returns>
    public Task StartDistanceSensorDataStream();

    /// <summary>
    /// Stop the continuous distance measurement data stream.
    /// </summary>
    /// <returns>The completed Task.</returns>
    public Task StopDistanceSensorDataStream();

    /// <summary>
    /// Get a singular distance measurement.
    /// </summary>
    /// <returns>The completed Task.</returns>
    public Task GetDistanceMeasurement();

    /// <summary>
    /// Star the continuous lumen sensor data stream.
    /// </summary>
    /// <returns>The completed Task.</returns>
    public Task StartLumenSensorDataStream();

    /// <summary>
    /// Stop the continuous lumen sensor data stream.
    /// </summary>
    /// <returns></returns>
    public Task StopLumenSensorDataStream();

    /// <summary>
    /// Get a singular lumen measurement.
    /// </summary>
    /// <returns>The completed Task.</returns>
    public Task GetLumenMeasurement();

    /// <summary>
    /// Start the continuous temperature sensor data stream.
    /// </summary>
    /// <returns>The completed Task.</returns>
    public Task StartTemperatureSensorDataStream();

    /// <summary>
    /// Stop the continuous temperature sensor data stream.
    /// </summary>
    /// <returns>The completed Task.</returns>
    public Task StopTemperatureSensorDataStream();

    /// <summary>
    /// Get a singular temperature measurement.
    /// </summary>
    /// <returns>The completed Task.</returns>
    public Task GetTemperatureMeasurement();

    /// <summary>
    /// Get all pending device notifications.
    /// </summary>
    /// <returns>A dictionary containing all un-acknowledged device notifications. TODO flo fragen</returns>
    public Task<Dictionary<int, FeatherSdkNotification>> GetNotifications();

    /// <summary>
    /// Acknowledge a device notification.
    /// </summary>
    /// <param name="notificationId">The ID of the notification.</param>
    /// <returns>The completed Task.</returns>
    public Task SendNotificationAcknowledge(int notificationId);
}