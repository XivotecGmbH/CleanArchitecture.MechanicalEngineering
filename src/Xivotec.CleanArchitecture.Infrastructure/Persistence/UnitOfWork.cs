using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Domain.Common;
using Xivotec.CleanArchitecture.Infrastructure.Exceptions;
using Xivotec.CleanArchitecture.Infrastructure.Persistence.Common.Interfaces;

namespace Xivotec.CleanArchitecture.Infrastructure.Persistence;

/// <inheritdoc cref="IUnitOfWork"/>
public sealed class UnitOfWork : IUnitOfWork
{
    private readonly IDataContext _dataContext;

    private readonly Dictionary<Type, object> _repositories = new();

    public UnitOfWork(
        IDataContext dataContext,
        IEnumerable<IPersistentRepository> persistentRepositories,
        IEnumerable<IRuntimeRepository> runTimeRepositories
        )
    {
        _dataContext = dataContext;

        foreach (var repo in persistentRepositories)
        {
            AddRepositoryToDictionary(repo);
        }

        foreach (var repo in runTimeRepositories)
        {
            AddRepositoryToDictionary(repo);
        }
    }

    public void Dispose()
    {
        _dataContext.Dispose();
    }

    public IRelationalRepository<TEntity> GetRelationalRepository<TEntity>() where TEntity : Entity
    {
        var type = typeof(TEntity);
        if (_repositories.TryGetValue(type, out var repository))
        {
            return (IRelationalRepository<TEntity>)repository;
        }

        throw new RepositoryNotFoundException();
    }

    public ITimeSeriesRepository<TTimeSeriesEntry> GetTimeSeriesRepository<TTimeSeriesEntry>() where TTimeSeriesEntry : TimeSeriesEntry
    {
        var type = typeof(TTimeSeriesEntry);
        if (_repositories.TryGetValue(type, out var repository))
        {
            return (ITimeSeriesRepository<TTimeSeriesEntry>)repository;
        }

        throw new RepositoryNotFoundException();
    }

    /// <summary>
    /// Adds repository types for internal references.
    /// </summary>
    /// <param name="repo">Repository to add</param>
    private void AddRepositoryToDictionary(object repo)
    {
        var genArg = GetRepositoryGenericType(repo);

        if (genArg is not null)
        {
            _repositories.Add(genArg, repo);
        }
    }

    /// <summary>
    /// Returns the generic type TEntity of an <see cref="IRelationalRepository{TEntity}"/>
    /// if the repos implements it, null otherwise.
    /// </summary>
    /// <param name="repo">Repo to check.</param>
    private Type? GetRepositoryGenericType(object repo)
    {
        var typ = repo.GetType();

        foreach (var intType in typ.GetInterfaces())
        {
            if (!intType.IsGenericType)
            {
                continue;
            }

            var genType = intType.GetGenericTypeDefinition();

            if (genType == typeof(IRelationalRepository<>) || genType == typeof(ITimeSeriesRepository<>))
            {
                return intType.GetGenericArguments()[0];
            }
        }
        return null;
    }
}
