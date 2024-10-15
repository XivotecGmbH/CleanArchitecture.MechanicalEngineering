using Xivotec.CleanArchitecture.Application.TemperatureFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.TemperatureAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.UnitTests.TemperatureFeature.Common;

internal class TemperatureFeatureTestObjects
{
    private readonly DateTime _temperatureEntryDate = new(2024, 1, 1, 1, 1, 1);

    public List<TemperatureEntry> TemperatureEntries { get; init; }
    public List<TemperatureEntryDto> TemperatureEntryDtos { get; init; }

    public TemperatureFeatureTestObjects()
    {
        TemperatureEntries = new List<TemperatureEntry>()
        {
            new()
            {
                Timestamp = _temperatureEntryDate,
                Temperature = 1.0d,
                Source = "1"
            },
            new()
            {
                Timestamp = _temperatureEntryDate,
                Temperature = 2.0d,
                Source = "2"
            }
        };

        TemperatureEntryDtos = new List<TemperatureEntryDto>()
        {
            new()
            {
                Timestamp = _temperatureEntryDate,
                Temperature = 1.0d,
                Source = "1"
            },
            new()
            {
                Timestamp = _temperatureEntryDate,
                Temperature = 2.0d,
                Source = "2"
            }
        };
    }
}