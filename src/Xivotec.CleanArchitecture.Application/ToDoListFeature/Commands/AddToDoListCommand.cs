using AutoMapper;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;

/// <summary>
/// Command which saves a <see cref="ToDoListDto"/> to the repository.
/// </summary>
/// <param name="item">The <see cref="ToDoListDto"/> to be saved.</param>
public record AddToDoListCommand(ToDoListDto item) : IRequest<ToDoListDto>;

public class AddToDoListHandler : IRequestHandler<AddToDoListCommand, ToDoListDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AddToDoListHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ToDoListDto> Handle(AddToDoListCommand request, CancellationToken cancellationToken)
    {
        var itemToAdd = _mapper.Map<ToDoList>(request.item);
        var repo = _unitOfWork.GetRepository<ToDoList>();

        await repo.AddAsync(itemToAdd);
        return request.item;
    }
}