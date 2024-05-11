using Xivotec.CleanArchitecture.Domain.Common;
using Xivotec.CleanArchitecture.Domain.FeatherDeviceAggregate.Enums;

namespace Xivotec.CleanArchitecture.Domain.FeatherDeviceAggregate.Entities;

public class FeatherDevice : DeviceEntity
{
    public string ComPort { get; set; } = string.Empty;

    public List<LedColor> AvailableLedColors { get; set; } = [];
}