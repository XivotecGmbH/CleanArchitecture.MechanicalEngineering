using Xivotec.CleanArchitecture.Application.Common.Device;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;

namespace Xivotec.CleanArchitecture.Application.Services.Device;

public interface IFeatherDeviceService : IDeviceService
{
    Task InitialiseAsync(FeatherDeviceDto device);

    Task DeinitializeAsync(FeatherDeviceDto device);

    Task StartAsync(FeatherDeviceDto device);

    Task StartTemperatureDataReceivingAsync(FeatherDeviceDto device);

    Task StartDistanceDataReceivingAsync(FeatherDeviceDto device);

    Task StartLumenDataReceivingAsync(FeatherDeviceDto device);

    Task StopAsync(FeatherDeviceDto device);

    Task ConnectAsync(FeatherDeviceDto device);

    Task DisconnectAsync(FeatherDeviceDto device);

    Task PauseAsync(FeatherDeviceDto device);

    Task ContinueAsync(FeatherDeviceDto device);

    Task<ConnectionStateDto> GetConnectionStateAsync(FeatherDeviceDto device);
}
