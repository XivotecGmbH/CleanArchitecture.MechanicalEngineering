using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Messages;
using Xivotec.CleanArchitecture.Application.Messages;
using Xivotec.CleanArchitecture.Presentation.Server.Services.Signal.Hubs;
using Xivotec.CleanArchitecture.Presentation.Server.Services.Signal.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection RegisterAngularServerServices(this IServiceCollection services)
    {
        services.AddTransient<ISignalHub<NotificationMessage>, NotificationHub>();
        services.AddTransient<ISignalHub<ConnectionStateChangedMessage>, DeviceConnectionStateHub>();
        services.AddTransient<ISignalHub<DistanceDataChangedMessage>, DeviceDistanceDataHub>();
        services.AddTransient<ISignalHub<TemperatureDataChangedMessage>, DeviceTemperatureDataHub>();
        services.AddTransient<ISignalHub<LumenDataChangedMessage>, DeviceLumenDataHub>();
        services.AddOpenApiDocument();
        services.AddRouting(options => options.LowercaseUrls = true);
        return services;
    }
}