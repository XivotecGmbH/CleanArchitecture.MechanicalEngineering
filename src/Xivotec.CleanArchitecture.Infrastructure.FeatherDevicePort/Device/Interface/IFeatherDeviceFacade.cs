using Xivotec.CleanArchitecture.Domain.FeatherDeviceAggregate.Enums;

namespace Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Interface;
public interface IFeatherDeviceFacade
{
    Task InitialiseAsync(string port);

    Task DeinitializeAsync();

    Task ConnectAsync();

    Task DisconnectAsync();

    Task StartAsync();

    Task StartTemperatureSensorDataStream();

    Task StartDistanceSensorDataStream();

    Task StartLumenSensorDataStream();

    Task StopAsync();

    Task PauseAsync();

    Task ContinueAsync();

    ConnectionState GetConnectionSate();
}