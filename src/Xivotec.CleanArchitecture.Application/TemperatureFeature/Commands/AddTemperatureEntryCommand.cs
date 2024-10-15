using AutoMapper;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.TemperatureFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.TemperatureAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.TemperatureFeature.Commands;

/// <summary>
/// Command which saves a <see cref="TemperatureEntryDto"/> to the bucket.
/// </summary>
/// <param name="Entry">The <see cref="TemperatureEntryDto"/> to be saved.</param>
public record AddTemperatureEntryCommand(TemperatureEntryDto Entry) : IRequest<TemperatureEntryDto>;

public class AddTemperatureEntryHandler : IRequestHandler<AddTemperatureEntryCommand, TemperatureEntryDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddTemperatureEntryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TemperatureEntryDto> Handle(AddTemperatureEntryCommand request, CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetTimeSeriesRepository<TemperatureEntry>();
        var domainEntry = _mapper.Map<TemperatureEntry>(request.Entry);
        await repo.AddAsync(domainEntry);
        return request.Entry;
    }
}