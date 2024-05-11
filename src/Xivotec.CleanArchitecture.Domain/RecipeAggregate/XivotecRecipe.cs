using Xivotec.CleanArchitecture.Domain.Common;

namespace Xivotec.CleanArchitecture.Domain.RecipeAggregate;

public class XivotecRecipe : RecipeEntity
{
    public string Name { get; set; } = string.Empty;

    public virtual FeatherDeviceRecipe? FeatherDeviceRecipe { get; set; }
}