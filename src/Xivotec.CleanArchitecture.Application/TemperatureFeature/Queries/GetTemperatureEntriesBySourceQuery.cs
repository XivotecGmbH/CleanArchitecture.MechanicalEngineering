using AutoMapper;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.TemperatureFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.TemperatureAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.TemperatureFeature.Queries;

/// <summary>
/// Query which returns all entries of <see cref="TemperatureEntryDto"/> in the bucket, filtered by a data source.
/// </summary>
/// <param name="Source">The query data source.</param>
public record GetTemperatureEntriesBySourceQuery(string Source) : IRequest<List<TemperatureEntryDto>>;

public class GetTemperatureEntriesBySourceQueryHandler : IRequestHandler<GetTemperatureEntriesBySourceQuery, List<TemperatureEntryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTemperatureEntriesBySourceQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<TemperatureEntryDto>> Handle(GetTemperatureEntriesBySourceQuery request, CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetTimeSeriesRepository<TemperatureEntry>();
        var results = await repo.GetBySource(request.Source);
        var resultDtos = results.Select(_mapper.Map<TemperatureEntryDto>);
        return resultDtos.ToList();
    }
}