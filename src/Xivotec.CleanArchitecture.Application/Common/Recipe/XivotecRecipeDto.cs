using Xivotec.CleanArchitecture.Application.FeatherRecipeFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.RecipeAggregate;

namespace Xivotec.CleanArchitecture.Application.Common.Recipe;

/// <summary>
/// DTO representing a <see cref="XivotecRecipe"/>
/// </summary>
public class XivotecRecipeDto
{
    public Guid Id { get; init; }

    public string Name { get; set; } = string.Empty;

    public FeatherDeviceRecipeDto? FeatherDeviceRecipe { get; set; }
}