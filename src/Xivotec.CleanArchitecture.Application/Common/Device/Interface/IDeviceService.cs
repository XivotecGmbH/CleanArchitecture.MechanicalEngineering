namespace Xivotec.CleanArchitecture.Application.Common.Device.Interface;

/// <summary>
/// Generic device service.
/// </summary>
/// <typeparam name="TDevice">The type of device entity DTO.</typeparam>
public interface IDeviceService<TDevice> : IDeviceServiceBase
{
    /// <summary>
    /// Init the device.
    /// </summary>
    /// <param name="device">The device entity DTO of the device to be initialized.</param>
    /// <returns>The completed task.</returns>
    Task InitializeAsync(TDevice device);

    /// <summary>
    /// Clean up resources of the device.
    /// </summary>
    /// <param name="device">The device entity DTO of the device to be de-initialized.</param>
    /// <returns>The completed task.</returns>
    Task DeinitializeAsync(TDevice device);

    /// <summary>
    /// Connect to the device.
    /// </summary>
    /// <param name="device">The device entity DTO of the device to connect to.</param>
    /// <returns>The completed task.</returns>
    Task ConnectAsync(TDevice device);

    /// <summary>
    /// Disconnect from the device.
    /// </summary>
    /// <param name="device">The device entity DTO of the device to disconnect from.</param>
    /// <returns>The completed task.</returns>
    Task DisconnectAsync(TDevice device);

    /// <summary>
    /// Apply a config to a given device.
    /// </summary>
    /// <param name="device">The device entity DTO for which the recipe should be applied, also includes the recipe.</param>
    /// <returns>The completed task.</returns>
    Task ApplyConfigAsync(TDevice device);

    /// <summary>
    /// Get the current config of the specified device.
    /// </summary>
    /// <param name="device">The device entity DTO of the device to read the config from.</param>
    /// <returns>The device entity DTO containing the current configuration.</returns>
    Task<TDevice> GetCurrentConfigAsync(TDevice device);

    /// <summary>
    /// Generic write method to facilitate device-specific operations.
    /// </summary>
    /// <param name="device">The device entity DTO of the device to write to.</param>
    /// <returns>The completed task.</returns>
    Task WriteDataAsync(TDevice device);

    /// <summary>
    /// Generic read method to facilitate device-specific operations.
    /// </summary>
    /// <param name="device">The device entity DTO of the device to write to.</param>
    /// <returns>The read data.</returns>
    Task<TDevice> ReadDataAsync(TDevice device);
}