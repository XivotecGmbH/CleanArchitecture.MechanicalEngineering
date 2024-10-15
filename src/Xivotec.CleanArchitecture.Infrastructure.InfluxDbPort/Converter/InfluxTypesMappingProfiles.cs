using AutoMapper;
using Xivotec.CleanArchitecture.Domain.TemperatureAggregate.Entities;
using Xivotec.CleanArchitecture.Infrastructure.InfluxDbPort.InfluxTypes;

namespace Xivotec.CleanArchitecture.Infrastructure.InfluxDbPort.Converter;

public class InfluxTypesMappingProfiles : Profile
{
    /// <summary>
    /// Defines AutoMapper Profiles for InfluxDB specific types.
    /// </summary>
    public InfluxTypesMappingProfiles()
    {
        _ = CreateMap<TemperatureEntry, TemperatureEntryInfluxType>().ReverseMap();
    }
}