using AutoMapper;
using Microsoft.Extensions.Configuration;
using Xivotec.CleanArchitecture.Domain.TemperatureAggregate.Entities;
using Xivotec.CleanArchitecture.Infrastructure.InfluxDbPort;
using Xivotec.CleanArchitecture.Infrastructure.InfluxDbPort.Common;
using Xivotec.CleanArchitecture.Infrastructure.InfluxDbPort.InfluxTypes;

namespace Xivotec.CleanArchitecture.Infrastructure.UnitTests.Persistence.Repositories.Common;

public class TestTimeSeriesRepository : InfluxDbPersistentRepository<TemperatureEntry, TemperatureEntryInfluxType>
{
    public TestTimeSeriesRepository(IMapper mapper,
        InfluxDbPortDataContext context,
        IConfiguration configuration)
        : base(mapper,
        context,
        configuration)
    {
    }
}