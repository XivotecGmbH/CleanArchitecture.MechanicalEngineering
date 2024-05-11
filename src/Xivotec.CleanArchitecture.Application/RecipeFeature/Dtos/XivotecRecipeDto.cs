namespace Xivotec.CleanArchitecture.Application.RecipeFeature.Dtos;

public class XivotecRecipeDto
{
    public Guid Id { get; init; }

    public string Name { get; set; } = string.Empty;

    public FeatherDeviceRecipeDto? FeatherDeviceRecipe { get; set; }
}