using System.Reflection;
using Xivotec.CleanArchitecture.Application.Common.DependencyInjection;
using Xivotec.CleanArchitecture.Infrastructure.InfluxDbPort;
using Xivotec.CleanArchitecture.Infrastructure.Persistence.Common.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection RegisterInfluxDbPortServices(this IServiceCollection services)
    {
        services
            .RegisterInterfaceImplementationAsSingletons<IPersistentRepository>()
            .RegisterInterfaceImplementationAsSingletons<IRuntimeRepository>();

        services.AddSingleton<InfluxDbPortDataContext>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}