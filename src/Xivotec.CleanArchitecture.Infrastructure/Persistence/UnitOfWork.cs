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

    /// <inheritdoc cref="IUnitOfWork.GetRepository{TEntity}()"/>
    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity
    {
        var type = typeof(TEntity);
        if (_repositories.TryGetValue(type, out var repository))
        {
            return (IRepository<TEntity>)repository;
        }

        throw new RepositoryNotFoundException();
    }

    /// <summary>
    /// Adds IRepository Type to the internal references.
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
    /// Returns the generictype TEntity of an <see cref="IRepository{TEntity}"/>
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

            if (genType == typeof(IRepository<>))
            {
                return intType.GetGenericArguments()[0];
            }
        }
        return null;
    }
}
