using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.RecipeAggregate;

namespace Xivotec.CleanArchitecture.Application.FeatherRecipeFeature.Dtos;

/// <summary>
/// DTO representing a <see cref="FeatherDeviceRecipe"/>
/// </summary>
public class FeatherDeviceRecipeDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Interval { get; set; }

    public LedColorDto LedColor { get; set; }

    public bool IsLedSwitchedOn { get; set; }

    public bool IsDisplaySwitchedOn { get; set; }
}
