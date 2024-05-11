using AutoMapper;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.FeatherDeviceAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Commands;

/// <summary>
/// Command which updates the requested <see cref="FeatherDeviceDto"/> in the repository.
/// </summary>
/// <param name="device">The <see cref="FeatherDeviceDto"/> to be updated.</param>
public record UpdateFeatherDeviceCommand(FeatherDeviceDto device) : IRequest;

public class UpdateFeatherDeviceHandler : IRequestHandler<UpdateFeatherDeviceCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateFeatherDeviceHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateFeatherDeviceCommand request, CancellationToken cancellationToken)
    {
        var device = _mapper.Map<FeatherDevice>(request.device);
        var repo = _unitOfWork.GetRepository<FeatherDevice>();

        await repo.UpdateAsync(device);
    }
}
