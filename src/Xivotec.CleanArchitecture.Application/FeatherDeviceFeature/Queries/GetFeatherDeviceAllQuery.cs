using AutoMapper;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.FeatherDeviceAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Queries;

/// <summary>
/// Query which returns all entries of <see cref="FeatherDeviceDto"/> in the repository.
/// </summary>
public record GetFeatherDeviceAllQuery : IRequest<List<FeatherDeviceDto>>;

public class GetFeatherDeviceAllQueryHandler : IRequestHandler<GetFeatherDeviceAllQuery, List<FeatherDeviceDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetFeatherDeviceAllQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<FeatherDeviceDto>> Handle(GetFeatherDeviceAllQuery request, CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetRepository<FeatherDevice>();
        var lists = await repo.GetAllAsync();

        var res = lists.Select(_mapper.Map<FeatherDeviceDto>);
        return res.ToList();
    }
}
