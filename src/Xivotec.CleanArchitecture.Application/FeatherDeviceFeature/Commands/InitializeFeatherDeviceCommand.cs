using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Device.Interface;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;

namespace Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Commands;

/// <summary>
/// Command which initializes to a <see cref="FeatherDeviceDto"/>.
/// </summary>
/// <param name="FeatherDevice">The target <see cref="FeatherDeviceDto"/>.</param>
public record InitializeFeatherDeviceCommand(FeatherDeviceDto FeatherDevice) : IRequest;

public class InitializeFeatherDeviceHandler : IRequestHandler<InitializeFeatherDeviceCommand>
{
    private readonly IDeviceUnitOfWork _deviceUnitOfWork;

    public InitializeFeatherDeviceHandler(IDeviceUnitOfWork deviceUnitOfWork)
    {
        _deviceUnitOfWork = deviceUnitOfWork;
    }

    public async Task Handle(InitializeFeatherDeviceCommand request, CancellationToken cancellationToken)
    {
        var deviceService = _deviceUnitOfWork.GetDeviceService<FeatherDeviceDto>();
        await deviceService.InitializeAsync(request.FeatherDevice);
    }
}