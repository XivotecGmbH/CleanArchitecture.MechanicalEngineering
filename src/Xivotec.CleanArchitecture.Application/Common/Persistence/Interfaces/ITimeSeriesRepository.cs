using Xivotec.CleanArchitecture.Domain.Common;

namespace Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;

/// <summary>
/// Generic Time Series Repository interface. Normally not used directly.
/// </summary>
/// <typeparam name="TTimeSeriesEntry">Entry the Repository takes.</typeparam>
public interface ITimeSeriesRepository<TTimeSeriesEntry> where TTimeSeriesEntry : TimeSeriesEntry
{
    /// <summary>
    /// Gets all entries stored inside the repository.
    /// </summary>
    /// <returns>All found entries.</returns>
    Task<List<TTimeSeriesEntry>> GetAllAsync();

    /// <summary>
    /// Gets all entries from a specified time period.
    /// </summary>
    /// <param name="start">The start time</param>
    /// <param name="stop">The end time</param>
    /// <returns>All found entries.</returns>
    Task<List<TTimeSeriesEntry>> GetInRangeAsync(DateTime start, DateTime stop);

    /// <summary>
    /// Gets all entries stored from a specified data source.
    /// </summary>
    /// <param name="series">The data source</param>
    /// <returns>All found entries.</returns>
    Task<List<TTimeSeriesEntry>> GetBySource(string series);

    /// <summary>
    /// Gets all entries from a specified data source, within a specified time period.
    /// </summary>
    /// <param name="start">The start time</param>
    /// <param name="stop">The end time</param>
    /// <param name="series">The data source</param>
    /// <returns>All found entries.</returns>
    Task<List<TTimeSeriesEntry>> GetBySourceInRange(DateTime start, DateTime stop, string series);

    /// <summary>
    /// Saves a singular entry.
    /// </summary>
    /// <param name="item">The item to be saved</param>
    /// <returns>The completed task.</returns>
    Task AddAsync(TTimeSeriesEntry item);

    /// <summary>
    /// Saves a singular entry using batching.
    /// </summary>
    /// <param name="item">The item to be saved</param>
    /// <returns>The completed task.</returns>
    Task AddBatchedAsync(TTimeSeriesEntry item);

    /// <summary>
    /// Saves a range of entries.
    /// </summary>
    /// <param name="items">The items to be saved</param>
    /// <returns>The completed task.</returns>
    Task AddRangeAsync(List<TTimeSeriesEntry> items);

    /// <summary>
    /// Saves a range of entries using batching.
    /// </summary>
    /// <param name="items">The items to be saved</param>
    /// <returns>The completed task.</returns>
    Task AddRangeBatchedAsync(List<TTimeSeriesEntry> items);

    /// <summary>
    /// Deletes a range of entries.
    /// </summary>
    /// <param name="start">The start time</param>
    /// <param name="stop">The end time</param>
    /// <returns>The completed task.</returns>
    Task DeleteRangeAsync(DateTime start, DateTime stop);
}