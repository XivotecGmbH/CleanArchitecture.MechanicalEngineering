using Xivotec.CleanArchitecture.Application.Common.Recipe;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Application.FeatherRecipeFeature.Dtos;

namespace Xivotec.CleanArchitecture.Infrastructure.UnitTests.Devices.Common;

internal class XivotecRecipeTestableObjects
{
    public readonly FeatherDeviceRecipeDto FeatherDeviceRecipeDto;
    public readonly XivotecRecipeDto XivotecRecipeDto;

    public XivotecRecipeTestableObjects()
    {
        FeatherDeviceRecipeDto = new()
        {
            Id = Guid.NewGuid(),
            Name = "Sarah Connor",
            LedColor = LedColorDto.Green,
            IsDisplaySwitchedOn = false,
            IsLedSwitchedOn = false,
        };
        XivotecRecipeDto = new()
        {
            Id = Guid.NewGuid(),
            Name = "Terminator",
            FeatherDeviceRecipe = FeatherDeviceRecipeDto,
        };
    }
}
