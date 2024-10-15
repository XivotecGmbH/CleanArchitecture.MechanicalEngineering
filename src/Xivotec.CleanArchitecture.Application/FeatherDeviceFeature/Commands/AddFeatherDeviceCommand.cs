using AutoMapper;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.FeatherDeviceAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Commands;

/// <summary>
/// Command which saves a <see cref="FeatherDeviceDto"/> to the repository.
/// </summary>
/// <param name="FeatherDevice">The <see cref="FeatherDeviceDto"/> to be saved.</param>
public record AddFeatherDeviceCommand(FeatherDeviceDto FeatherDevice) : IRequest<FeatherDeviceDto>;

public class AddFeatherDeviceHandler : IRequestHandler<AddFeatherDeviceCommand, FeatherDeviceDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AddFeatherDeviceHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<FeatherDeviceDto> Handle(AddFeatherDeviceCommand request, CancellationToken cancellationToken)
    {
        var itemToAdd = _mapper.Map<FeatherDevice>(request.FeatherDevice);
        var repo = _unitOfWork.GetRelationalRepository<FeatherDevice>();

        await repo.AddAsync(itemToAdd);
        return _mapper.Map<FeatherDeviceDto>(itemToAdd);
    }
}
