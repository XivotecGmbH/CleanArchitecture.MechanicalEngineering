using InfluxDB.Client.Core;

namespace Xivotec.CleanArchitecture.Infrastructure.InfluxDbPort.Common.Interfaces;

/// <summary>
/// Base InfluxDB Port Entity type. Used for database queries.
/// </summary>
public interface IInfluxPortType
{
    [Column(IsTimestamp = true)]
    public DateTime Timestamp { get; set; }

    [Column("source", IsTag = true)]
    public string Source { get; set; }
}