using Xivotec.CleanArchitecture.Application.Common.Recipe;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.FeatherDeviceAggregate.Entities;
using Xivotec.CleanArchitecture.Domain.FeatherDeviceAggregate.Enums;

namespace Xivotec.CleanArchitecture.Application.UnitTests.FeatherDeviceFeature.Common;

internal class FeatherDeviceFeatureTestObjects
{
    public readonly FeatherDeviceDto FeatherDeviceDto = new()
    {
        Id = Guid.NewGuid(),
        Name = "DeviceZero",
        ComPort = "42",
        ConnectionState = ConnectionStateDto.Disconnected
    };

    public readonly List<FeatherDevice> FeatherDevices = new()
    {
        new FeatherDevice
        {
            Id = Guid.NewGuid(),
            Name = "DeviceZero",
            ComPort= "42",
            ConnectionState = ConnectionState.Disconnected
        },

        new FeatherDevice
        {
            Id = Guid.NewGuid(),
            Name = "DeviceOne",
            ComPort= "43",
            ConnectionState = ConnectionState.Disconnected
        }
    };

    public readonly XivotecRecipeDto XivotecRecipeDto = new()
    {
        Id = Guid.NewGuid(),
        Name = "RecipeOne",
        FeatherDeviceRecipe = new()
        {
            Id = Guid.NewGuid(),
            Name = "FeatherRecipeOne",
            LedColor = LedColorDto.Green,
            Interval = 1,
            IsDisplaySwitchedOn = true,
            IsLedSwitchedOn = true
        }
    };
}
