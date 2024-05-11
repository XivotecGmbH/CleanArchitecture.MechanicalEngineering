using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Infrastructure.Devices.Facade;

namespace Xivotec.CleanArchitecture.Infrastructure.Devices.Factory;

/// <summary>
/// FeatherDeviceFactory. Creates the FeatherDeviceFacade
/// </summary>
public interface IFeatherDeviceFactory
{
    /// <summary>
    /// Creates the FeatherDeviceFacade
    /// </summary>
    /// <param name="featherDevice">specifies with Id which facade is created</param>
    FeatherDeviceFacade CreateFeatherDeviceFacade(FeatherDeviceDto featherDevice);
}
