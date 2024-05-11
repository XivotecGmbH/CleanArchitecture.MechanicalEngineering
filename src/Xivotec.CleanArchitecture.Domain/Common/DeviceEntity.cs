using Xivotec.CleanArchitecture.Domain.FeatherDeviceAggregate.Enums;

namespace Xivotec.CleanArchitecture.Domain.Common;
public abstract class DeviceEntity : Entity
{
    public string? Name { get; set; }

    public ConnectionState ConnectionState { get; set; }

    public string ConnectionString { get; set; } = string.Empty;
}
