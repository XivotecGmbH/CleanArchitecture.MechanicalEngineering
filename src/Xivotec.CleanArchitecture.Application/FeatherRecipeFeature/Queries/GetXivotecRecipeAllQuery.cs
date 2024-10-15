using AutoMapper;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.Common.Recipe;
using Xivotec.CleanArchitecture.Domain.RecipeAggregate;

namespace Xivotec.CleanArchitecture.Application.FeatherRecipeFeature.Queries;

/// <summary>
/// Query which returns all entries of <see cref="XivotecRecipeDto"/> in the repository.
/// </summary>
public record GetXivotecRecipeAllQuery : IRequest<List<XivotecRecipeDto>>;

public class GetXivotecRecipeAllQueryHandler : IRequestHandler<GetXivotecRecipeAllQuery, List<XivotecRecipeDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetXivotecRecipeAllQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<XivotecRecipeDto>> Handle(GetXivotecRecipeAllQuery request, CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetRelationalRepository<XivotecRecipe>();
        var recipes = await repo.GetAllAsync();

        var res = recipes.Select(_mapper.Map<XivotecRecipeDto>);
        return res.ToList();
    }
}
