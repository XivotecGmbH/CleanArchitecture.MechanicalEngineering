using AutoMapper;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.TemperatureFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.TemperatureAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.TemperatureFeature.Commands;

/// <summary>
/// Command which saves a list of <see cref="TemperatureEntryDto"/> to the bucket.
/// </summary>
/// <param name="Entries">The list of <see cref="TemperatureEntryDto"/> to be saved.</param>
public record AddTemperatureEntriesCommand(List<TemperatureEntryDto> Entries) : IRequest<List<TemperatureEntryDto>>;

public class AddTemperatureEntriesHandler : IRequestHandler<AddTemperatureEntriesCommand, List<TemperatureEntryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddTemperatureEntriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<TemperatureEntryDto>> Handle(AddTemperatureEntriesCommand request, CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetTimeSeriesRepository<TemperatureEntry>();
        var domainEntries = request.Entries.Select(_mapper.Map<TemperatureEntry>);
        await repo.AddRangeAsync(domainEntries.ToList());
        return request.Entries;
    }
}