using System.Text.Json.Serialization;
using Xivotec.CleanArchitecture.Presentation.Angular.Server.Services.Signal;
using Xivotec.CleanArchitecture.Presentation.Angular.Server.Services.Signal.Hubs;
using Xivotec.CleanArchitecture.Presentation.Angular.Server.Setup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .RegisterSerilog()
    .AddCoreServices()
    .RegisterInfrastructureServices()
    .RegisterDevicePortServices()
    .RegisterPostgreSqlPortServices()
    .RegisterInfluxDbPortServices()
    .RegisterAngularServerServices()
    .AddHostedService<SignalBackgroundService>()
    .AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddSignalR();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseOpenApi();
app.UseSwaggerUi();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.MapHub<NotificationHub>("/api/hubs/notification");
app.MapHub<DeviceConnectionStateHub>("/api/hubs/device/connectionstate");
app.MapHub<DeviceDistanceDataHub>("/api/hubs/device/distance");
app.MapHub<DeviceTemperatureDataHub>("/api/hubs/device/temperature");
app.MapHub<DeviceLumenDataHub>("/api/hubs/device/lumen");

app.Run();