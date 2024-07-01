using CommunityToolkit.Maui;
using Serilog;
using Xivotec.CleanArchitecture.Presentation.Maui.Exceptions;
using Xivotec.CleanArchitecture.Presentation.Maui.Setup;

namespace Xivotec.CleanArchitecture.Presentation.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            })
            .UseMauiCommunityToolkit();

        builder.Services
            .RegisterConfiguration()
            .RegisterSerilog()
            .AddCoreServices()
            .RegisterPresentationModels()
            .RegisterPresentationServices()
            .RegisterInfrastructureServices()
            .RegisterDevicePortServices()
            .RegisterPostgreSqlPortServices()
            .RegisterRoutes();

        MauiGlobalExceptionDelegate.UnhandledException += OnUnhandledException;

        return builder.Build();
    }

    private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        var ex = e.ExceptionObject as Exception;
        var errorMessage = $"An unhandled exception occurred: {ex!.Message}";

        // Only log unhandled exception for now. No application shut down.
        Log.Error(errorMessage, ex);
    }
}