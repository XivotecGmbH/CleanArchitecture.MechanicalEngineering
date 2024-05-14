using Xivotec.CleanArchitecture.Application.Common.DependencyInjection;
using Xivotec.CleanArchitecture.Application.Services.Device;
using Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Facade;
using Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Factory;
using Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Interface;
using Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Sdk;
using Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Service;
using Xivotec.CleanArchitecture.Infrastructure.Persistence.Common.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection RegisterDevicePortServices(this IServiceCollection services)
    {
        services
            .RegisterInterfaceImplementationAsTransient<IDeviceService>()
            .RegisterInterfaceImplementationAsSingletons<IRuntimeRepository>();

        services.AddTransient<IFeatherDeviceService, FeatherDeviceService>();
        services.AddTransient<IFeatherDeviceFacade, FeatherDeviceFacade>();
        services.AddTransient<IFeatherDeviceFactory, FeatherDeviceFacadeFactory>();
        services.AddTransient<IFeatherSdk, FeatherSdk>();

        return services;
    }
}