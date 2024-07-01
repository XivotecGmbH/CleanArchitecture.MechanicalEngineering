using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Device.Interface;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Messages;

namespace Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Commands;

/// <summary>
/// Command which disconnects to a <see cref="FeatherDeviceDto"/>.
/// </summary>
/// <param name="FeatherDevice">The target <see cref="FeatherDeviceDto"/>.</param>
public record DisconnectFeatherDeviceCommand(FeatherDeviceDto FeatherDevice) : IRequest;

public class DisconnectFeatherDeviceHandler : IRequestHandler<DisconnectFeatherDeviceCommand>
{
    private readonly IDeviceUnitOfWork _deviceUnitOfWork;
    private readonly IMediator _mediator;

    public DisconnectFeatherDeviceHandler(IDeviceUnitOfWork deviceUnitOfWork, IMediator mediator)
    {
        _deviceUnitOfWork = deviceUnitOfWork;
        _mediator = mediator;
    }

    public async Task Handle(DisconnectFeatherDeviceCommand request, CancellationToken cancellationToken)
    {
        var deviceService = _deviceUnitOfWork.GetDeviceService<FeatherDeviceDto>();
        await deviceService.DisconnectAsync(request.FeatherDevice);

        request.FeatherDevice.ConnectionState = ConnectionStateDto.Disconnected;
        await _mediator.Send(new UpdateFeatherDeviceCommand(request.FeatherDevice), CancellationToken.None);

        WeakReferenceMessenger.Default.Send(new ConnectionStateChangedMessage(false));
    }
}