namespace Xivotec.CleanArchitecture.Domain.Common;

/// <summary>
/// Time series entry base class.
/// </summary>
public abstract class TimeSeriesEntry
{
    /// <summary>
    /// Required Timestamp
    /// </summary>
    public DateTime Timestamp { get; init; }
}