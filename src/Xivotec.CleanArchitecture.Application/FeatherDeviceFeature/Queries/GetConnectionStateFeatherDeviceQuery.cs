using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Device.Interface;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;

namespace Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Queries;

/// <summary>
/// Query which returns the connection state of the requested <see cref="FeatherDeviceDto"/>.
/// </summary>
/// <param name="FeatherDevice">The requested <see cref="FeatherDeviceDto"/></param>
public record GetConnectionStateFeatherDeviceQuery(FeatherDeviceDto FeatherDevice) : IRequest<ConnectionStateDto>;

public class GetConnectionStateFeatherDeviceHandler : IRequestHandler<GetConnectionStateFeatherDeviceQuery, ConnectionStateDto>
{
    private readonly IDeviceUnitOfWork _deviceUnitOfWork;

    public GetConnectionStateFeatherDeviceHandler(IDeviceUnitOfWork deviceUnitOfWork)
    {
        _deviceUnitOfWork = deviceUnitOfWork;
    }

    public async Task<ConnectionStateDto> Handle(GetConnectionStateFeatherDeviceQuery request, CancellationToken cancellationToken)
    {
        var deviceService = _deviceUnitOfWork.GetDeviceService<FeatherDeviceDto>();
        request.FeatherDevice.Action = FeatherDeviceActionsDto.GetConnectionState;

        var result = await deviceService.ReadDataAsync(request.FeatherDevice);
        return result.ConnectionState;
    }
}