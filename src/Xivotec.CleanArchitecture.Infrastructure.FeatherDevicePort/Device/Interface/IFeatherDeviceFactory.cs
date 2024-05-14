using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;

namespace Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Interface;

/// <summary>
/// FeatherDeviceFactory. Creates the FeatherDeviceFacade
/// </summary>
public interface IFeatherDeviceFactory
{
    /// <summary>
    /// Creates the FeatherDeviceFacade
    /// </summary>
    /// <param name="featherDevice">specifies with Id which facade is created</param>
    IFeatherDeviceFacade CreateFeatherDeviceFacade(FeatherDeviceDto featherDevice);
}
