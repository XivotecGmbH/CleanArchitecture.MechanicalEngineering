using InfluxDB.Client.Core;
using Xivotec.CleanArchitecture.Domain.TemperatureAggregate.Entities;
using Xivotec.CleanArchitecture.Infrastructure.InfluxDbPort.Common.Interfaces;

namespace Xivotec.CleanArchitecture.Infrastructure.InfluxDbPort.InfluxTypes;

/// <summary>
/// InfluxDB Port Type representing a <see cref="TemperatureEntry"/>
/// </summary>
[Measurement("temperature")]
public class TemperatureEntryInfluxType : IInfluxPortType
{
    [Column(IsTimestamp = true)]
    public DateTime Timestamp { get; set; }

    [Column("source", IsTag = true)]
    public string Source { get; set; } = string.Empty;

    [Column("temp", IsTag = false)]
    public double Temperature { get; set; }
}