using Xivotec.CleanArchitecture.Domain.Common;

namespace Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;

/// <summary>
/// UnitOfWork. Manages all interactions between Database and Repositories
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Returns Repository specified by TEntity/>
    /// </summary>
    /// <typeparam name="TEntity"> Item the repository contains.</typeparam>
    /// <exception cref="RepositoryNotFoundException"> No repository found.</exception>> 
    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity;
}