using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Device.Interface;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Messages;

namespace Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Commands;

/// <summary>
/// Command which connects to a <see cref="FeatherDeviceDto"/>.
/// </summary>
/// <param name="FeatherDevice">The target <see cref="FeatherDeviceDto"/>.</param>
public record ConnectFeatherDeviceCommand(FeatherDeviceDto FeatherDevice) : IRequest;

public class ConnectFeatherDeviceHandler : IRequestHandler<ConnectFeatherDeviceCommand>
{
    private readonly IDeviceUnitOfWork _deviceUnitOfWork;
    private readonly IMediator _mediator;

    public ConnectFeatherDeviceHandler(IDeviceUnitOfWork deviceUnitOfWork, IMediator mediator)
    {
        _deviceUnitOfWork = deviceUnitOfWork;
        _mediator = mediator;
    }

    public async Task Handle(ConnectFeatherDeviceCommand request, CancellationToken cancellationToken)
    {
        var deviceService = _deviceUnitOfWork.GetDeviceService<FeatherDeviceDto>();
        await deviceService.ConnectAsync(request.FeatherDevice);

        request.FeatherDevice.ConnectionState = ConnectionStateDto.Connected;
        await _mediator.Send(new UpdateFeatherDeviceCommand(request.FeatherDevice), CancellationToken.None);

        WeakReferenceMessenger.Default.Send(new ConnectionStateChangedMessage(true));
    }
}