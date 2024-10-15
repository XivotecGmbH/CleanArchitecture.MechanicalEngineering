using AutoMapper;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.TemperatureFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.TemperatureAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.TemperatureFeature.Commands;

/// <summary>
/// Command which saves a list of <see cref="TemperatureEntryDto"/> to the bucket using batching.
/// </summary>
/// <param name="Entries">The list of <see cref="TemperatureEntryDto"/> to be saved.</param>
public record AddTemperatureEntriesBatchedCommand(List<TemperatureEntryDto> Entries) : IRequest<List<TemperatureEntryDto>>;

public class AddTemperatureEntriesBatchedHandler : IRequestHandler<AddTemperatureEntriesBatchedCommand, List<TemperatureEntryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddTemperatureEntriesBatchedHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<TemperatureEntryDto>> Handle(AddTemperatureEntriesBatchedCommand request, CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetTimeSeriesRepository<TemperatureEntry>();
        var domainEntries = request.Entries.Select(_mapper.Map<TemperatureEntry>);
        await repo.AddRangeBatchedAsync(domainEntries.ToList());
        return request.Entries;
    }
}