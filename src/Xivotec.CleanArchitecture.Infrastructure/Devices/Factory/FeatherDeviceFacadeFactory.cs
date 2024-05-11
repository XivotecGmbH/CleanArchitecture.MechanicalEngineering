using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Infrastructure.Devices.Facade;

namespace Xivotec.CleanArchitecture.Infrastructure.Devices.Factory;

public class FeatherDeviceFacadeFactory : IFeatherDeviceFactory
{
    public FeatherDeviceFacade CreateFeatherDeviceFacade(FeatherDeviceDto featherDevice)
    {
        return new FeatherDeviceFacade { Id = featherDevice.Id };
    }
}
