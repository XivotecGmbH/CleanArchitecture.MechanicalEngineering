using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.TemperatureFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.TemperatureAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.TemperatureFeature.Commands;

/// <summary>
/// Command which deletes the requested range of <see cref="TemperatureEntryDto"/> from the bucket.
/// </summary>
/// <param name="Start">The delete range start time. Should be the earlier one of the two <see cref="DateTime"/> parameters.</param>
/// <param name="Stop">The delete range end time.</param>
public record DeleteTemperatureEntryRangeCommand(DateTime Start, DateTime Stop) : IRequest;

public class DeleteTemperatureEntryRangeHandler : IRequestHandler<DeleteTemperatureEntryRangeCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTemperatureEntryRangeHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task Handle(DeleteTemperatureEntryRangeCommand request, CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetTimeSeriesRepository<TemperatureEntry>();
        await repo.DeleteRangeAsync(request.Start, request.Stop);
    }
}