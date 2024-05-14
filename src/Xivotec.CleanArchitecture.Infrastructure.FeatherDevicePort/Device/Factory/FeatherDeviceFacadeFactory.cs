using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Facade;
using Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Interface;

namespace Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Factory;

public class FeatherDeviceFacadeFactory : IFeatherDeviceFactory
{
    private readonly IFeatherSdk _featherSdk;

    public FeatherDeviceFacadeFactory(IFeatherSdk featherSdk)
    {
        _featherSdk = featherSdk;
    }

    public IFeatherDeviceFacade CreateFeatherDeviceFacade(FeatherDeviceDto featherDevice)
    {
        return new FeatherDeviceFacade(_featherSdk)
        {
            Id = featherDevice.Id
        };
    }
}
