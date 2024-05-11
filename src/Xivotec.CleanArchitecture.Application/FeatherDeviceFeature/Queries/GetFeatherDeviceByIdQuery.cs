using AutoMapper;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.FeatherDeviceAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Queries;

/// <summary>
/// Query which returns the requested <see cref="FeatherDeviceDto"/>.
/// </summary>
/// <param name="Id">The ID of the requested <see cref="FeatherDeviceDto"/></param>
public record GetFeatherDeviceByIdQuery(Guid Id) : IRequest<FeatherDeviceDto>;

public class GetFeatherDeviceByIdQueryHandler : IRequestHandler<GetFeatherDeviceByIdQuery, FeatherDeviceDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetFeatherDeviceByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<FeatherDeviceDto> Handle(GetFeatherDeviceByIdQuery request, CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetRepository<FeatherDevice>();
        var device = await repo.GetByIdAsync(request.Id);

        return _mapper.Map<FeatherDeviceDto>(device);
    }
}
