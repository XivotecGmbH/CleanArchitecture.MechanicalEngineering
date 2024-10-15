using AutoMapper;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.ToDoListFeature.Queries;

/// <summary>
/// Query which returns all entries of <see cref="ToDoListDto"/> in the repository.
/// </summary>
public record GetToDoItemAllQuery : IRequest<List<ToDoItemDto>>;

public class GetToDoItemListAllQueryHandler : IRequestHandler<GetToDoItemAllQuery, List<ToDoItemDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetToDoItemListAllQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ToDoItemDto>> Handle(GetToDoItemAllQuery request, CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetRelationalRepository<ToDoItem>();
        var lists = await repo.GetAllAsync();

        var res = lists.Select(_mapper.Map<ToDoItemDto>);
        return res.ToList();
    }
}