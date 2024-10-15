using AutoMapper;
using Microsoft.Extensions.Configuration;
using Xivotec.CleanArchitecture.Domain.TemperatureAggregate.Entities;
using Xivotec.CleanArchitecture.Infrastructure.InfluxDbPort.Common;
using Xivotec.CleanArchitecture.Infrastructure.InfluxDbPort.InfluxTypes;

namespace Xivotec.CleanArchitecture.Infrastructure.InfluxDbPort.Repositories;

public class TemperatureRepository(
    IMapper mapper,
    InfluxDbPortDataContext context,
    IConfiguration configuration)
    : InfluxDbPersistentRepository<TemperatureEntry, TemperatureEntryInfluxType>(
        mapper,
        context,
        configuration);