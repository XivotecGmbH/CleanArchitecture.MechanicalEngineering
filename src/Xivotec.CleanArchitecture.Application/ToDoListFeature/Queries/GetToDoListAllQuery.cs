using AutoMapper;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.ToDoListFeature.Queries;

/// <summary>
/// Query which returns all entries of <see cref="ToDoListDto"/> in the repository.
/// </summary>
public record GetToDoListAllQuery : IRequest<List<ToDoListDto>>;

public class GetToDoListListHandler : IRequestHandler<GetToDoListAllQuery, List<ToDoListDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetToDoListListHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ToDoListDto>> Handle(GetToDoListAllQuery request, CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetRepository<ToDoList>();
        var lists = await repo.GetAllAsync();

        return lists.Select(_mapper.Map<ToDoListDto>).ToList();
    }
}