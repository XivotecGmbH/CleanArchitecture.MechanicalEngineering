using AutoMapper;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;

/// <summary>
/// Command which deletes the requested <see cref="ToDoItemDto"/> from the repository.
/// </summary>
/// <param name="Item">The <see cref="ToDoItemDto"/> to be deleted.</param>
public record DeleteToDoItemCommand(ToDoItemDto Item) : IRequest;

public class DeleteToDoItemHandler : IRequestHandler<DeleteToDoItemCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteToDoItemHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
    {
        var itemToDelete = _mapper.Map<ToDoItem>(request.Item);

        var repo = _unitOfWork.GetRelationalRepository<ToDoItem>();
        await repo.DeleteAsync(itemToDelete);
    }
}