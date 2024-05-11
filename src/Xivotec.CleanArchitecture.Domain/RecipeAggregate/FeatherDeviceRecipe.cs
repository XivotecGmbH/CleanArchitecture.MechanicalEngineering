using Xivotec.CleanArchitecture.Domain.Common;
using Xivotec.CleanArchitecture.Domain.FeatherDeviceAggregate.Enums;

namespace Xivotec.CleanArchitecture.Domain.RecipeAggregate;

public class FeatherDeviceRecipe : RecipeEntity
{
    public string Name { get; set; } = string.Empty;

    public int Interval { get; set; }

    public LedColor LedColor { get; set; }

    public bool IsLedSwitchedOn { get; set; }

    public bool IsDisplaySwitchedOn { get; set; }
}
