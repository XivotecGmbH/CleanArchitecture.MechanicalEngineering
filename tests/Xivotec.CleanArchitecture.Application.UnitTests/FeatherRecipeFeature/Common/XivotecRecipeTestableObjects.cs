using Xivotec.CleanArchitecture.Application.Common.Recipe;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Application.FeatherRecipeFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.FeatherDeviceAggregate.Enums;
using Xivotec.CleanArchitecture.Domain.RecipeAggregate;

namespace Xivotec.CleanArchitecture.Application.UnitTests.FeatherRecipeFeature.Common;

internal class XivotecRecipeTestableObjects
{
    public readonly FeatherDeviceRecipeDto FeatherDeviceRecipeDto;
    public readonly XivotecRecipeDto XivotecRecipeDto;

    public readonly FeatherDeviceRecipe FeatherDeviceRecipe;
    public readonly XivotecRecipe XivotecRecipe;

    public readonly List<XivotecRecipe> XivotecRecipes;
    public readonly List<XivotecRecipeDto> XivotecRecipeDtos;

    public XivotecRecipeTestableObjects()
    {
        FeatherDeviceRecipe = new()
        {
            Id = Guid.NewGuid(),
            Name = "Sarah Connor",
            LedColor = LedColor.Green,
            IsDisplaySwitchedOn = false,
            IsLedSwitchedOn = false,
        };
        XivotecRecipe = new()
        {
            Id = Guid.NewGuid(),
            Name = "Terminator",
            FeatherDeviceRecipe = FeatherDeviceRecipe,
        };

        var featherDeviceRecipe = new FeatherDeviceRecipe()
        {
            Id = Guid.NewGuid(),
            Name = "John Connor",
            LedColor = LedColor.Green,
            IsDisplaySwitchedOn = false,
            IsLedSwitchedOn = false,
        };

        var xivoRecipe = new XivotecRecipe()
        {
            Id = Guid.NewGuid(),
            Name = "Terminator",
            FeatherDeviceRecipe = featherDeviceRecipe,
        };

        XivotecRecipes =
        [
            XivotecRecipe,
            xivoRecipe
        ];

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

        var featherDeviceRecipeDto = new FeatherDeviceRecipeDto()
        {
            Id = Guid.NewGuid(),
            Name = "John Connor",
            LedColor = LedColorDto.Green,
            IsDisplaySwitchedOn = false,
            IsLedSwitchedOn = false,
        };

        var xivotecRecipeDto = new XivotecRecipeDto()
        {
            Id = Guid.NewGuid(),
            Name = "Terminator",
            FeatherDeviceRecipe = featherDeviceRecipeDto,
        };

        XivotecRecipeDtos =
        [
            XivotecRecipeDto,
            xivotecRecipeDto,
        ];
    }
}
