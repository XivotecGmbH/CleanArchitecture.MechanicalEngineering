using AutoMapper;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.TemperatureFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.TemperatureAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.TemperatureFeature.Queries;

/// <summary>
/// Query which returns all entries of <see cref="TemperatureEntryDto"/> in the bucket.
/// </summary>
public record GetTemperatureEntriesAllQuery : IRequest<List<TemperatureEntryDto>>;

public class GetTemperatureEntriesAllQueryHandler : IRequestHandler<GetTemperatureEntriesAllQuery, List<TemperatureEntryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTemperatureEntriesAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<TemperatureEntryDto>> Handle(GetTemperatureEntriesAllQuery request, CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetTimeSeriesRepository<TemperatureEntry>();
        var results = await repo.GetAllAsync();
        var resultDtos = results.Select(_mapper.Map<TemperatureEntryDto>);
        return resultDtos.ToList();
    }
}