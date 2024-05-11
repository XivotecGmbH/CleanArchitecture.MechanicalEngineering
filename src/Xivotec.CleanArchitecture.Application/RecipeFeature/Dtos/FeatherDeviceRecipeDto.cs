using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;

namespace Xivotec.CleanArchitecture.Application.RecipeFeature.Dtos;

public class FeatherDeviceRecipeDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Interval { get; set; }

    public LedColorDto LedColor { get; set; }

    public bool IsLedSwitchedOn { get; set; }

    public bool IsDisplaySwitchedOn { get; set; }
}
