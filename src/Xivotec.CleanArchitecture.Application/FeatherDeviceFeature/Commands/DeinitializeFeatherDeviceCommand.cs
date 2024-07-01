using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Device.Interface;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;

namespace Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Commands;

/// <summary>
/// Command which de-initializes a <see cref="FeatherDeviceDto"/>.
/// </summary>
/// <param name="FeatherDevice">The target <see cref="FeatherDeviceDto"/>.</param>
public record DeinitializeFeatherDeviceCommand(FeatherDeviceDto FeatherDevice) : IRequest;

public class DeinitializeFeatherDeviceHandler : IRequestHandler<DeinitializeFeatherDeviceCommand>
{
    private readonly IDeviceUnitOfWork _deviceUnitOfWork;

    public DeinitializeFeatherDeviceHandler(IDeviceUnitOfWork deviceUnitOfWork)
    {
        _deviceUnitOfWork = deviceUnitOfWork;
    }

    public async Task Handle(DeinitializeFeatherDeviceCommand request, CancellationToken cancellationToken)
    {
        var deviceService = _deviceUnitOfWork.GetDeviceService<FeatherDeviceDto>();
        await deviceService.DeinitializeAsync(request.FeatherDevice);
    }
}