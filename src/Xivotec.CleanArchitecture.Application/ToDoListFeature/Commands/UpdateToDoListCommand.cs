using AutoMapper;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;

/// <summary>
/// Command which updates the requested <see cref="ToDoListDto"/> in the repository.
/// </summary>
/// <param name="item">The <see cref="ToDoListDto"/> to be updated.</param>
public record UpdateToDoListCommand(ToDoListDto item) : IRequest;

public class UpdateToDoListHandler : IRequestHandler<UpdateToDoListCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateToDoListHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateToDoListCommand request, CancellationToken cancellationToken)
    {
        var itemToUpdate = _mapper.Map<ToDoList>(request.item);
        var repo = _unitOfWork.GetRepository<ToDoList>();

        await repo.UpdateAsync(itemToUpdate);
    }
}