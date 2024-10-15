using Xivotec.CleanArchitecture.Domain.Common;

namespace Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;

/// <summary>
/// UnitOfWork. Manages all interactions between Database and Repositories
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Returns a Relational Repository specified by TEntity./>
    /// </summary>
    /// <typeparam name="TEntity">Item the repository contains.</typeparam>
    /// <returns>The found repository</returns>
    public IRelationalRepository<TEntity> GetRelationalRepository<TEntity>() where TEntity : Entity;

    /// <summary>
    /// Returns a Time Series Repository specified by TEntity.
    /// </summary>
    /// <typeparam name="TTimeSeriesEntry">Entry the repository contains.</typeparam>
    /// <returns>The found repository</returns>
    public ITimeSeriesRepository<TTimeSeriesEntry> GetTimeSeriesRepository<TTimeSeriesEntry>()
        where TTimeSeriesEntry : TimeSeriesEntry;
}