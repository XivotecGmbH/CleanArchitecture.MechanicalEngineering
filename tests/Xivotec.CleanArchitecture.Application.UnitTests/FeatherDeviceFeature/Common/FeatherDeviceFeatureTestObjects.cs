using Xivotec.CleanArchitecture.Application.Common.Device;
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
}
