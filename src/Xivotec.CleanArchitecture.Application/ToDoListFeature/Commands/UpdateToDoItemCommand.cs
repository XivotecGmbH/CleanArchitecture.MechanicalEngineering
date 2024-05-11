using AutoMapper;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;

/// <summary>
/// Command which updates the requested <see cref="ToDoItemDto"/> in the repository.
/// </summary>
/// <param name="item">The <see cref="ToDoListDto"/> to be updated.</param>
public record UpdateToDoItemCommand(ToDoItemDto item) : IRequest;

public class UpdateToDoItemHandler : IRequestHandler<UpdateToDoItemCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateToDoItemHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateToDoItemCommand request, CancellationToken cancellationToken)
    {
        var itemToUpdate = _mapper.Map<ToDoItem>(request.item);
        var repo = _unitOfWork.GetRepository<ToDoItem>();

        await repo.UpdateAsync(itemToUpdate);
    }
}