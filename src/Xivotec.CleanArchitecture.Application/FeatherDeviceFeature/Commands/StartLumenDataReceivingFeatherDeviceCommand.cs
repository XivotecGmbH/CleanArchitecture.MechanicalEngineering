using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Device.Interface;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;

namespace Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Commands;

/// <summary>
/// Command which starts a lumen sensor stream on a <see cref="FeatherDeviceDto"/>.
/// </summary>
/// <param name="FeatherDevice">The target <see cref="FeatherDeviceDto"/>.</param>
public record StartLumenDataReceivingFeatherDeviceCommand(FeatherDeviceDto FeatherDevice) : IRequest;

public class StartLumenDataReceivingFeatherDeviceHandler : IRequestHandler<StartLumenDataReceivingFeatherDeviceCommand>
{
    private readonly IDeviceUnitOfWork _deviceUnitOfWork;

    public StartLumenDataReceivingFeatherDeviceHandler(IDeviceUnitOfWork deviceUnitOfWork)
    {
        _deviceUnitOfWork = deviceUnitOfWork;
    }

    public async Task Handle(StartLumenDataReceivingFeatherDeviceCommand request, CancellationToken cancellationToken)
    {
        var deviceService = _deviceUnitOfWork.GetDeviceService<FeatherDeviceDto>();
        request.FeatherDevice.Action = FeatherDeviceActionsDto.StartLumenDataReceiving;
        await deviceService.WriteDataAsync(request.FeatherDevice);
    }
}