using AutoMapper;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.TemperatureFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.TemperatureAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.TemperatureFeature.Commands;

/// <summary>
/// Command which saves a <see cref="TemperatureEntryDto"/> to the bucket using batching.
/// </summary>
/// <param name="Entry">The <see cref="TemperatureEntryDto"/> to be saved.</param>
public record AddTemperatureEntryBatchedCommand(TemperatureEntryDto Entry) : IRequest<TemperatureEntryDto>;

public class AddTemperatureEntryBatchedHandler : IRequestHandler<AddTemperatureEntryBatchedCommand, TemperatureEntryDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddTemperatureEntryBatchedHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TemperatureEntryDto> Handle(AddTemperatureEntryBatchedCommand request, CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetTimeSeriesRepository<TemperatureEntry>();
        var domainEntry = _mapper.Map<TemperatureEntry>(request.Entry);
        await repo.AddBatchedAsync(domainEntry);
        return request.Entry;
    }
}