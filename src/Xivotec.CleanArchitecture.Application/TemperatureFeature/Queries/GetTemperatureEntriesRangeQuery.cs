﻿using AutoMapper;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.TemperatureFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.TemperatureAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.TemperatureFeature.Queries;

/// <summary>
/// Query which returns all entries of <see cref="TemperatureEntryDto"/> in the bucket, filtered by a time range.
/// </summary>
/// <param name="Start">The query range start time. Should be the earlier one of the two <see cref="DateTime"/> parameters.</param>
/// <param name="Stop">The query range end time.</param>
public record GetTemperatureEntriesRangeQuery(DateTime Start, DateTime Stop) : IRequest<List<TemperatureEntryDto>>;

public class GetTemperatureEntriesRangeQueryHandler : IRequestHandler<GetTemperatureEntriesRangeQuery, List<TemperatureEntryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTemperatureEntriesRangeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<TemperatureEntryDto>> Handle(GetTemperatureEntriesRangeQuery request, CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetTimeSeriesRepository<TemperatureEntry>();
        var results = await repo.GetInRangeAsync(request.Start, request.Stop);
        var resultDtos = results.Select(_mapper.Map<TemperatureEntryDto>);
        return resultDtos.ToList();
    }
}