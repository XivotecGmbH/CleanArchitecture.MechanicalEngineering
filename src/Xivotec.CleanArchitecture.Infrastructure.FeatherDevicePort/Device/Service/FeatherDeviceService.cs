using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Device;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.Exceptions;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Commands;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Application.Services.Device;
using Xivotec.CleanArchitecture.Infrastructure.Exceptions;
using Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Facade;
using Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Interface;
using Xivotec.CleanArchitecture.Infrastructure.Messages.ConnectionState;
using Xivotec.CleanArchitecture.Infrastructure.Messages.Data;

namespace Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Service;

public class FeatherDeviceService : IFeatherDeviceService,
    IRecipient<DistanceDataReceivedMessage>,
    IRecipient<LumenDataReceivedMessage>,
    IRecipient<TemperatureDataReceivedMessage>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFeatherDeviceFactory _featherDeviceFactory;
    private readonly IMediator _mediator;

    public FeatherDeviceService(IUnitOfWork unitOfWork,
        IFeatherDeviceFactory featherDeviceFactory,
        IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _featherDeviceFactory = featherDeviceFactory;
        _mediator = mediator;

        WeakReferenceMessenger.Default.RegisterAll(this);
    }

    public async Task InitialiseAsync(FeatherDeviceDto device)
    {
        var repo = _unitOfWork.GetRepository<FeatherDeviceFacade>();
        var facade = _featherDeviceFactory.CreateFeatherDeviceFacade(device);
        await repo.AddAsync((FeatherDeviceFacade)facade);

        await facade.InitialiseAsync(device.ComPort);
    }

    public async Task DeinitializeAsync(FeatherDeviceDto device)
    {
        var facade = await GetFacadeAsync(device);
        await facade.DisconnectAsync();
        await DeleteFacadeAsync(device);
    }

    public async Task ConnectAsync(FeatherDeviceDto device)
    {
        var facade = await GetFacadeAsync(device);

        await facade.ConnectAsync();
        device.ConnectionState = ConnectionStateDto.Connected;
        await _mediator.Send(new UpdateFeatherDeviceCommand(device));
        WeakReferenceMessenger.Default.Send(new ConnectionStateChangedMessage(true));
    }

    public async Task DisconnectAsync(FeatherDeviceDto device)
    {
        var facade = await GetFacadeAsync(device);

        await facade.DisconnectAsync();
        device.ConnectionState = ConnectionStateDto.Disconnected;
        await _mediator.Send(new UpdateFeatherDeviceCommand(device));
        WeakReferenceMessenger.Default.Send(new ConnectionStateChangedMessage(false));
    }

    public async Task StartAsync(FeatherDeviceDto device)
    {
        var facade = await GetFacadeAsync(device);

        await facade.StartAsync();
    }

    public async Task StartTemperatureDataReceivingAsync(FeatherDeviceDto device)
    {
        var facade = await GetFacadeAsync(device);

        await facade.StartTemperatureSensorDataStream();
    }

    public async Task StartDistanceDataReceivingAsync(FeatherDeviceDto device)
    {
        var facade = await GetFacadeAsync(device);

        await facade.StartDistanceSensorDataStream();
    }

    public async Task StartLumenDataReceivingAsync(FeatherDeviceDto device)
    {
        var facade = await GetFacadeAsync(device);

        await facade.StartLumenSensorDataStream();
    }

    public async Task StopAsync(FeatherDeviceDto device)
    {
        var facade = await GetFacadeAsync(device);

        await facade.StopAsync();
    }

    public async Task PauseAsync(FeatherDeviceDto device)
    {
        var facade = await GetFacadeAsync(device);

        await facade.PauseAsync();
    }

    public async Task ContinueAsync(FeatherDeviceDto device)
    {
        var facade = await GetFacadeAsync(device);

        await facade.ContinueAsync();
    }
    public async Task<ConnectionStateDto> GetConnectionStateAsync(FeatherDeviceDto device)
    {
        var facade = await GetFacadeAsync(device);
        return (ConnectionStateDto)facade.GetConnectionSate();
    }

    public void Receive(DistanceDataReceivedMessage message)
        => WeakReferenceMessenger.Default.Send(new DistanceDataChangedMessage(message.Value));

    public void Receive(LumenDataReceivedMessage message)
        => WeakReferenceMessenger.Default.Send(new LumenDataChangedMessage(message.Value));

    public void Receive(TemperatureDataReceivedMessage message)
        => WeakReferenceMessenger.Default.Send(new TemperatureDataChangedMessage(message.Value));

    private async Task<FeatherDeviceFacade> GetFacadeAsync(FeatherDeviceDto device)
    {
        var repo = _unitOfWork.GetRepository<FeatherDeviceFacade>();
        try
        {
            return await repo.GetByIdAsync(device.Id);
        }
        catch (ItemNotFoundException)
        {
            throw new FacadeNotFoundException($"No facade found with ID {device.Id}");
        }
    }

    private async Task DeleteFacadeAsync(FeatherDeviceDto device)
    {
        var repo = _unitOfWork.GetRepository<FeatherDeviceFacade>();
        try
        {
            var deviceToDelete = await repo.GetByIdAsync(device.Id);
            await deviceToDelete.DeinitializeAsync();
            await repo.DeleteAsync(deviceToDelete);
        }
        catch (ItemNotFoundException)
        {
            throw new FacadeNotFoundException($"No facade found with ID {device.Id}");
        }
    }
}