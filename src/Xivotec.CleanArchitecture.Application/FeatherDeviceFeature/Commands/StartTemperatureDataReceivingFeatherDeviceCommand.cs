using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Device.Interface;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;

namespace Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Commands;

/// <summary>
/// Command which starts a temperature sensor stream on a <see cref="FeatherDeviceDto"/>.
/// </summary>
/// <param name="FeatherDevice">The target <see cref="FeatherDeviceDto"/>.</param>
public record StartTemperatureDataReceivingFeatherDeviceCommand(FeatherDeviceDto FeatherDevice) : IRequest;

public class StartTemperatureDataReceivingFeatherDeviceHandler : IRequestHandler<StartTemperatureDataReceivingFeatherDeviceCommand>
{
    private readonly IDeviceUnitOfWork _deviceUnitOfWork;

    public StartTemperatureDataReceivingFeatherDeviceHandler(IDeviceUnitOfWork deviceUnitOfWork)
    {
        _deviceUnitOfWork = deviceUnitOfWork;
    }

    public async Task Handle(StartTemperatureDataReceivingFeatherDeviceCommand request, CancellationToken cancellationToken)
    {
        var deviceService = _deviceUnitOfWork.GetDeviceService<FeatherDeviceDto>();
        request.FeatherDevice.Action = FeatherDeviceActionsDto.StartTemperatureDataReceiving;
        await deviceService.WriteDataAsync(request.FeatherDevice);
    }
}