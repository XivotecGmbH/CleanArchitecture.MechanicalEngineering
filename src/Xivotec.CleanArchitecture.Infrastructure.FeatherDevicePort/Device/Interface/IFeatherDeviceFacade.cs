using Xivotec.CleanArchitecture.Application.Common.Recipe;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;

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

    public Task LoadRecipeAsync(XivotecRecipeDto recipe);

    public Task<XivotecRecipeDto> GetCurrentRecipeAsync();

    ConnectionStateDto GetConnectionSate();
}