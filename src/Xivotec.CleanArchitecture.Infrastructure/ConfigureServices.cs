using Xivotec.CleanArchitecture.Application.Common.DependencyInjection;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.Notifications.Service;
using Xivotec.CleanArchitecture.Application.RecipeFeature;
using Xivotec.CleanArchitecture.Application.Services.PersistenceConfiguration;
using Xivotec.CleanArchitecture.Application.Services.Time;
using Xivotec.CleanArchitecture.Infrastructure.Notifications.Service;
using Xivotec.CleanArchitecture.Infrastructure.Persistence;
using Xivotec.CleanArchitecture.Infrastructure.Persistence.Common.Interfaces;
using Xivotec.CleanArchitecture.Infrastructure.Recipe;
using Xivotec.CleanArchitecture.Infrastructure.Recipe.Interface;
using Xivotec.CleanArchitecture.Infrastructure.Services.PersistenceConfiguration;
using Xivotec.CleanArchitecture.Infrastructure.Services.SystemClock;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
    {
        services
            .RegisterInterfaceImplementationAsSingletons<IRuntimeRepository>();

        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IPersistenceConfigurationService, PersistenceConfigurationService>();
        services.AddTransient<ISystemClockService, SystemClockService>();
        services.AddTransient<INotificationService, NotificationService>();
        services.AddTransient<IRecipeImporter, RecipeImporter>();
        services.AddTransient<IRecipeExporter, RecipeExporter>();
        services.AddTransient<IRecipeService, RecipeService>();

        return services;
    }
}