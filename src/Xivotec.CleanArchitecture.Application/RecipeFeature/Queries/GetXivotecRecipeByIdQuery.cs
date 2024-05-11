using AutoMapper;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.RecipeFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.RecipeAggregate;

namespace Xivotec.CleanArchitecture.Application.RecipeFeature.Queries;

/// <summary>
/// Query which returns the requested <see cref="XivotecRecipeDto"/>.
/// </summary>
/// <param name="Id">The ID of the requested <see cref="XivotecRecipeDto"/></param>
public record GetXivotecRecipeByIdQuery(Guid Id) : IRequest<XivotecRecipeDto>;

public class GetXivotecRecipeByIdQueryHandler : IRequestHandler<GetXivotecRecipeByIdQuery, XivotecRecipeDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetXivotecRecipeByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<XivotecRecipeDto> Handle(GetXivotecRecipeByIdQuery request, CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetRepository<XivotecRecipe>();
        var recipe = await repo.GetByIdAsync(request.Id);

        return _mapper.Map<XivotecRecipeDto>(recipe);
    }
}
