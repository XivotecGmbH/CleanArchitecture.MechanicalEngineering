using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using System.Reflection;
using Xivotec.CleanArchitecture.Application.Common.Behaviors;
using Xivotec.CleanArchitecture.Application.Common.DependencyInjection;
using Xivotec.CleanArchitecture.Application.Common.Process;
using Xivotec.CleanArchitecture.Application.Common.Process.Interfaces;
using Xivotec.CleanArchitecture.Application.Services.DomainDispatcher;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            cfg.AddRequestPreProcessor(typeof(IRequestPreProcessor<>), typeof(LoggingBehaviour<>));
        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Process related registers
        services.AddTransient<IProcessRunner, ProcessRunner>();
        services.RegisterInterfaceImplementationAsTransient<IProcessAction>();
        services.RegisterInterfaceImplementationAsTransient<IProcessDefinition>();

        return services;
    }
}