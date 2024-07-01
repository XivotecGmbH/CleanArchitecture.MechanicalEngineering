using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Device.Interface;
using Xivotec.CleanArchitecture.Application.Common.Recipe;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;

namespace Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Queries;

/// <summary>
/// Query which returns the recipe of the requested <see cref="FeatherDeviceDto"/>.
/// </summary>
/// <param name="FeatherDevice">The <see cref="FeatherDeviceDto"/> for which the recipe is requested.</param>
public record GetCurrentFeatherDeviceRecipeQuery(FeatherDeviceDto FeatherDevice) : IRequest<XivotecRecipeDto>;

public class GetCurrentFeatherDeviceRecipeHandler : IRequestHandler<GetCurrentFeatherDeviceRecipeQuery, XivotecRecipeDto>
{
    private readonly IDeviceUnitOfWork _deviceUnitOfWork;

    public GetCurrentFeatherDeviceRecipeHandler(IDeviceUnitOfWork deviceUnitOfWork)
    {
        _deviceUnitOfWork = deviceUnitOfWork;
    }

    public async Task<XivotecRecipeDto> Handle(GetCurrentFeatherDeviceRecipeQuery request, CancellationToken cancellationToken)
    {
        var deviceService = _deviceUnitOfWork.GetDeviceService<FeatherDeviceDto>();

        var result = await deviceService.GetCurrentConfigAsync(request.FeatherDevice);
        return result.Recipe;
    }
}