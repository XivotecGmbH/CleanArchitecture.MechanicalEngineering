using AutoMapper;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Linq;
using Microsoft.Extensions.Configuration;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Domain.Common;
using Xivotec.CleanArchitecture.Infrastructure.InfluxDbPort.Common.Interfaces;
using Xivotec.CleanArchitecture.Infrastructure.Persistence.Common.Interfaces;

namespace Xivotec.CleanArchitecture.Infrastructure.InfluxDbPort.Common;

/// <inheritdoc cref="ITimeSeriesRepository{TEntity}"/>
public abstract class InfluxDbPersistentRepository<TEntity, TPortEntity>
    : ITimeSeriesRepository<TEntity>, IPersistentRepository
    where TEntity : TimeSeriesEntry
    where TPortEntity : IInfluxPortType
{
    private const int DefaultBatchSize = 10000;
    private readonly InfluxDbPortDataContext _context;
    private readonly IMapper _mapper;
    private readonly string _bucketName;
    private readonly int _batchSize;

    private List<TEntity> _batch = [];

    protected InfluxDbPersistentRepository(IMapper mapper,
        InfluxDbPortDataContext context,
        IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _bucketName = _context.InfluxTypesToBucketDictionary[typeof(TPortEntity)];

        var batchSizeFromConfig = configuration.GetConnectionString("InfluxBatchSize");
        _batchSize = !string.IsNullOrEmpty(batchSizeFromConfig) ? int.Parse(batchSizeFromConfig) : DefaultBatchSize;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        var settings = new QueryableOptimizerSettings
        {
            QueryMultipleTimeSeries = true

        };
        var linqQuery =
            from s in InfluxDBQueryable<TPortEntity>.Queryable(
                _bucketName,
                _context.Org,
                _context.Client!.GetQueryApi(),
                settings)
            orderby s.Timestamp
            select s;

        var enumerable = linqQuery.ToInfluxQueryable().GetAsyncEnumerator();
        var resultList = new List<TEntity>();
        await foreach (var entry in enumerable)
        {
            var mappedEntry = (TEntity)_mapper.Map(entry, typeof(TPortEntity), typeof(TEntity));
            resultList.Add(mappedEntry);
        }

        return resultList;
    }

    public async Task<List<TEntity>> GetInRangeAsync(DateTime start, DateTime stop)
    {
        var settings = new QueryableOptimizerSettings
        {
            QueryMultipleTimeSeries = true

        };
        var linqQuery =
            from s in InfluxDBQueryable<TPortEntity>.Queryable(
                _bucketName,
                _context.Org,
                _context.Client!.GetQueryApi(),
                settings)
            where s.Timestamp > start
            where s.Timestamp < stop
            orderby s.Timestamp
            select s;

        var enumerable = linqQuery.ToInfluxQueryable().GetAsyncEnumerator();
        var resultList = new List<TEntity>();
        await foreach (var entry in enumerable)
        {
            var mappedEntry = (TEntity)_mapper.Map(entry, typeof(TPortEntity), typeof(TEntity));
            resultList.Add(mappedEntry);
        }

        return resultList;
    }

    public async Task<List<TEntity>> GetBySource(string series)
    {
        var settings = new QueryableOptimizerSettings
        {
            QueryMultipleTimeSeries = true

        };
        var linqQuery =
            from s in InfluxDBQueryable<TPortEntity>.Queryable(
                _bucketName,
                _context.Org,
                _context.Client!.GetQueryApi(),
                settings)
            where s.Source == series
            orderby s.Timestamp
            select s;

        var enumerable = linqQuery.ToInfluxQueryable().GetAsyncEnumerator();
        var resultList = new List<TEntity>();
        await foreach (var entry in enumerable)
        {
            var mappedEntry = (TEntity)_mapper.Map(entry, typeof(TPortEntity), typeof(TEntity));
            resultList.Add(mappedEntry);
        }

        return resultList;
    }

    public async Task<List<TEntity>> GetBySourceInRange(DateTime start, DateTime stop, string series)
    {
        var settings = new QueryableOptimizerSettings
        {
            QueryMultipleTimeSeries = true

        };
        var linqQuery =
            from s in InfluxDBQueryable<TPortEntity>.Queryable(
                _bucketName,
                _context.Org,
                _context.Client!.GetQueryApi(),
                settings)
            where s.Timestamp > start
            where s.Timestamp < stop
            where s.Source == series
            orderby s.Timestamp
            select s;

        var enumerable = linqQuery.ToInfluxQueryable().GetAsyncEnumerator();
        var resultList = new List<TEntity>();
        await foreach (var entry in enumerable)
        {
            var mappedEntry = (TEntity)_mapper.Map(entry, typeof(TPortEntity), typeof(TEntity));
            resultList.Add(mappedEntry);
        }

        return resultList;
    }

    public async Task AddAsync(TEntity item)
    {
        var writeApi = _context.Client!.GetWriteApiAsync();
        var influxTypeItem = _mapper.Map(item, typeof(TEntity), typeof(TPortEntity));
        await writeApi.WriteMeasurementAsync(influxTypeItem, WritePrecision.Ms, _bucketName, _context.Org);
    }

    public async Task AddBatchedAsync(TEntity item)
    {
        List<TEntity> batchedItems;
        lock (_batch)
        {
            _batch.Add(item);

            if (_batch.Count < _batchSize)
            {
                return;
            }
            batchedItems = new List<TEntity>(_batch);
            _batch = [];
        }

        var writeApi = _context.Client!.GetWriteApiAsync();
        var influxTypes = batchedItems.Select(_mapper.Map<TPortEntity>).ToList();
        await writeApi.WriteMeasurementsAsync(influxTypes, WritePrecision.Ms, _bucketName, _context.Org);
    }

    public async Task AddRangeAsync(List<TEntity> items)
    {
        await Task.WhenAll(items.Select(AddAsync));
    }

    public async Task AddRangeBatchedAsync(List<TEntity> items)
    {
        await Task.WhenAll(items.Select(AddBatchedAsync));
    }

    public Task DeleteRangeAsync(DateTime start, DateTime stop)
    {
        var deleteApi = _context.Client!.GetDeleteApi();
        return Task.FromResult(deleteApi.Delete(start, stop, "", _bucketName, _context.Org));
    }
}