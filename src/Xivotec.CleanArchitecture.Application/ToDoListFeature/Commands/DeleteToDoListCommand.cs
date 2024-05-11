using AutoMapper;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.Services.DomainDispatcher;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Events;

namespace Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;

/// <summary>
/// Command which deletes the requested <see cref="ToDoListDto"/> from the repository.
/// </summary>
/// <param name="item">The <see cref="ToDoListDto"/> to be deleted.</param>
public record DeleteToDoListCommand(ToDoListDto Item) : IRequest;

public class DeleteToDoListHandler : IRequestHandler<DeleteToDoListCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public DeleteToDoListHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IDomainEventDispatcher domainEventDispatcher)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _domainEventDispatcher = domainEventDispatcher;
    }

    public async Task Handle(DeleteToDoListCommand request, CancellationToken cancellationToken)
    {
        var itemToDelete = _mapper.Map<ToDoList>(request.Item);
        var repo = _unitOfWork.GetRepository<ToDoList>();

        // Events: Delete all items before list
        var domainEvent = new ToDoListDeletedEvent(itemToDelete);
        await _domainEventDispatcher.RaiseDomainEvent(domainEvent);

        await repo.DeleteAsync(itemToDelete);
    }
}