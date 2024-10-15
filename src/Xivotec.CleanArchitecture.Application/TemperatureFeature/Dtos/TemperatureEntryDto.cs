using Xivotec.CleanArchitecture.Domain.TemperatureAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.TemperatureFeature.Dtos;

/// <summary>
/// DTO representing a <see cref="TemperatureEntry"/>.
/// </summary>
public class TemperatureEntryDto
{
    public DateTime Timestamp { get; set; }

    public string Source { get; set; } = string.Empty;

    public double Temperature { get; set; }
}