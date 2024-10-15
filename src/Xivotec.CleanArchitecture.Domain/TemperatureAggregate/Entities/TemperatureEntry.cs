using Xivotec.CleanArchitecture.Domain.Common;

namespace Xivotec.CleanArchitecture.Domain.TemperatureAggregate.Entities;

public class TemperatureEntry : TimeSeriesEntry
{
    /// <summary>
    /// The temperature reading of the entry.
    /// </summary>
    public double Temperature { get; set; }

    /// <summary>
    /// The source of the entry, for example a specific sensor.
    /// </summary>
    public string Source { get; set; } = string.Empty;
}