using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Device.Interface;
using Xivotec.CleanArchitecture.Application.Common.Recipe;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;

namespace Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Commands;

/// <summary>
/// Command which loads a <see cref="XivotecRecipeDto"/> on a <see cref="FeatherDeviceDto"/>.
/// </summary>
/// <param name="FeatherDevice">The target <see cref="FeatherDeviceDto"/>.</param>
public record LoadFeatherDeviceRecipeCommand(FeatherDeviceDto FeatherDevice) : IRequest;

public class LoadFeatherDeviceRecipeHandler : IRequestHandler<LoadFeatherDeviceRecipeCommand>
{
    private readonly IDeviceUnitOfWork _deviceUnitOfWork;

    public LoadFeatherDeviceRecipeHandler(IDeviceUnitOfWork deviceUnitOfWork)
    {
        _deviceUnitOfWork = deviceUnitOfWork;
    }

    public async Task Handle(LoadFeatherDeviceRecipeCommand request, CancellationToken cancellationToken)
    {
        var deviceService = _deviceUnitOfWork.GetDeviceService<FeatherDeviceDto>();
        await deviceService.ApplyConfigAsync(request.FeatherDevice);
    }
}