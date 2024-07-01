using CommunityToolkit.Mvvm.Messaging;
using Xivotec.CleanArchitecture.Application.Common.Device.Interface;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.Exceptions;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Infrastructure.Exceptions;
using Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Exceptions;
using Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Facade;
using Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Interface;

namespace Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Service;

public class FeatherDeviceService :
    IDeviceService<FeatherDeviceDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFeatherDeviceFactory _featherDeviceFactory;

    public FeatherDeviceService(IFeatherDeviceFactory featherDeviceFactory, IUnitOfWork unitOfWork)
    {
        _featherDeviceFactory = featherDeviceFactory;
        _unitOfWork = unitOfWork;

        WeakReferenceMessenger.Default.RegisterAll(this);
    }

    public async Task InitializeAsync(FeatherDeviceDto device)
    {
        var repo = _unitOfWork.GetRepository<FeatherDeviceFacade>();
        var facade = _featherDeviceFactory.CreateFeatherDeviceFacade(device);
        await repo.AddAsync((FeatherDeviceFacade)facade);

        await facade.InitialiseAsync(device.ComPort);
    }

    public async Task DeinitializeAsync(FeatherDeviceDto device)
    {
        var facade = await GetFacadeByIdAsync(device.Id);
        await facade.DisconnectAsync();
        await DeleteFacadeByIdAsync(device.Id);
    }

    public async Task ConnectAsync(FeatherDeviceDto device)
    {
        var facade = await GetFacadeByIdAsync(device.Id);
        await facade.ConnectAsync();
    }

    public async Task DisconnectAsync(FeatherDeviceDto device)
    {
        var facade = await GetFacadeByIdAsync(device.Id);
        await facade.DisconnectAsync();
    }

    public async Task ApplyConfigAsync(FeatherDeviceDto device)
    {
        var facade = await GetFacadeByIdAsync(device.Id);

        await facade.LoadRecipeAsync(device.Recipe);
    }

    public async Task<FeatherDeviceDto> GetCurrentConfigAsync(FeatherDeviceDto device)
    {
        var facade = await GetFacadeByIdAsync(device.Id);
        device.Recipe = await facade.GetCurrentRecipeAsync();

        return device;
    }

    public async Task WriteDataAsync(FeatherDeviceDto device)
    {
        switch (device.Action)
        {
            case FeatherDeviceActionsDto.Start:
                await StartAsync(device);
                break;
            case FeatherDeviceActionsDto.Stop:
                await StopAsync(device);
                break;
            case FeatherDeviceActionsDto.Pause:
                await PauseAsync(device);
                break;
            case FeatherDeviceActionsDto.Continue:
                await ContinueAsync(device);
                break;
            case FeatherDeviceActionsDto.StartTemperatureDataReceiving:
                await StartTemperatureDataReceivingAsync(device);
                break;
            case FeatherDeviceActionsDto.StartDistanceDataReceiving:
                await StartDistanceDataReceivingAsync(device);
                break;
            case FeatherDeviceActionsDto.StartLumenDataReceiving:
                await StartLumenDataReceivingAsync(device);
                break;
            default:
                throw new FeatherDeviceActionNotFoundException();
        }
    }

    public async Task<FeatherDeviceDto> ReadDataAsync(FeatherDeviceDto device)
    {
        switch (device.Action)
        {
            case FeatherDeviceActionsDto.GetConnectionState:
                device.ConnectionState = await GetConnectionStateAsync(device);
                return device;
            default:
                throw new FeatherDeviceActionNotFoundException();
        }
    }

    private async Task StartAsync(FeatherDeviceDto device)
    {
        var facade = await GetFacadeByIdAsync(device.Id);

        await facade.StartAsync();
    }

    private async Task StopAsync(FeatherDeviceDto device)
    {
        var facade = await GetFacadeByIdAsync(device.Id);

        await facade.StopAsync();
    }

    private async Task PauseAsync(FeatherDeviceDto device)
    {
        var facade = await GetFacadeByIdAsync(device.Id);

        await facade.PauseAsync();
    }

    private async Task ContinueAsync(FeatherDeviceDto device)
    {
        var facade = await GetFacadeByIdAsync(device.Id);

        await facade.ContinueAsync();
    }

    private async Task StartTemperatureDataReceivingAsync(FeatherDeviceDto device)
    {
        var facade = await GetFacadeByIdAsync(device.Id);

        await facade.StartTemperatureSensorDataStream();
    }

    private async Task StartDistanceDataReceivingAsync(FeatherDeviceDto device)
    {
        var facade = await GetFacadeByIdAsync(device.Id);

        await facade.StartDistanceSensorDataStream();
    }

    private async Task StartLumenDataReceivingAsync(FeatherDeviceDto device)
    {
        var facade = await GetFacadeByIdAsync(device.Id);

        await facade.StartLumenSensorDataStream();
    }

    private async Task<ConnectionStateDto> GetConnectionStateAsync(FeatherDeviceDto device)
    {
        var facade = await GetFacadeByIdAsync(device.Id);
        return facade.GetConnectionSate();
    }

    private async Task<FeatherDeviceFacade> GetFacadeByIdAsync(Guid id)
    {
        var repo = _unitOfWork.GetRepository<FeatherDeviceFacade>();
        try
        {
            return await repo.GetByIdAsync(id);
        }
        catch (ItemNotFoundException)
        {
            throw new FacadeNotFoundException($"No facade found with ID {id}");
        }
    }

    private async Task DeleteFacadeByIdAsync(Guid id)
    {
        var repo = _unitOfWork.GetRepository<FeatherDeviceFacade>();
        try
        {
            var deviceToDelete = await repo.GetByIdAsync(id);
            await deviceToDelete.DeinitializeAsync();
            await repo.DeleteAsync(deviceToDelete);
        }
        catch (ItemNotFoundException)
        {
            throw new FacadeNotFoundException($"No facade found with ID {id}");
        }
    }
}