using Xivotec.XivoBlue.MeadowSdk.DataTypes;

namespace Xivotec.CleanArchitecture.Infrastructure.Devices.Facade;
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

    ConnectionStates GetConnectionSate();
}