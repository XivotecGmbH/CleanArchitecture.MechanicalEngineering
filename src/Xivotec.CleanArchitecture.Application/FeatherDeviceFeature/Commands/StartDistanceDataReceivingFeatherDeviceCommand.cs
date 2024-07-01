using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Device.Interface;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;

namespace Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Commands;

/// <summary>
/// Command which starts a distance sensor stream on a <see cref="FeatherDeviceDto"/>.
/// </summary>
/// <param name="FeatherDevice">The target <see cref="FeatherDeviceDto"/>.</param>
public record StartDistanceDataReceivingFeatherDeviceCommand(FeatherDeviceDto FeatherDevice) : IRequest;

public class StartDistanceDataReceivingFeatherDeviceHandler : IRequestHandler<StartDistanceDataReceivingFeatherDeviceCommand>
{
    private readonly IDeviceUnitOfWork _deviceUnitOfWork;

    public StartDistanceDataReceivingFeatherDeviceHandler(IDeviceUnitOfWork deviceUnitOfWork)
    {
        _deviceUnitOfWork = deviceUnitOfWork;
    }

    public async Task Handle(StartDistanceDataReceivingFeatherDeviceCommand request, CancellationToken cancellationToken)
    {
        var deviceService = _deviceUnitOfWork.GetDeviceService<FeatherDeviceDto>();
        request.FeatherDevice.Action = FeatherDeviceActionsDto.StartDistanceDataReceiving;
        await deviceService.WriteDataAsync(request.FeatherDevice);
    }
}