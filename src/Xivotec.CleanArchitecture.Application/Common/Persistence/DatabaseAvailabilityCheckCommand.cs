using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Domain.TemperatureAggregate.Entities;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.Common.Persistence;

public record DatabaseAvailabilityCheckCommand : IRequest;

public class DatabaseAvailabilityCheckHandler : IRequestHandler<DatabaseAvailabilityCheckCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DatabaseAvailabilityCheckHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task Handle(DatabaseAvailabilityCheckCommand request, CancellationToken cancellationToken)
    {
        _ = _unitOfWork.GetRelationalRepository<ToDoList>();
        _ = _unitOfWork.GetTimeSeriesRepository<TemperatureEntry>();
        return Task.CompletedTask;
    }
}