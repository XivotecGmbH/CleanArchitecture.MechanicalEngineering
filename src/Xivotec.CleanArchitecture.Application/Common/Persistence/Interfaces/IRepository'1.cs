using Xivotec.CleanArchitecture.Domain.Common;

namespace Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;

/// <summary>
/// Generic Repository interface. Normally not used directly.
/// </summary>
/// <typeparam name="TEntity"> Entity the Repository takes.</typeparam>
public interface IRepository<TEntity> where TEntity : Entity
{
    /// <summary>
    /// Returns entity specified by id. Throws <see cref="Exceptions.ItemNotFoundException"/> if none was found.
    /// </summary>
    /// <param name="id"> Id of the entity</param>
    /// <exception cref="Exceptions.ItemNotFoundException"></exception>
    Task<TEntity> GetByIdAsync(Guid id);

    /// <summary>
    /// Returns all entities stored inside the repository.
    /// </summary>
    Task<List<TEntity>> GetAllAsync();

    /// <summary>
    /// Adds entity to the repository.
    /// </summary>
    /// <param name="item"> Entity to add.</param>
    /// <exception cref="Exceptions.RepositoryException"></exception>
    Task AddAsync(TEntity item);

    /// <summary>
    /// Updates entity.
    /// </summary>
    /// <param name="item"> Entity to update.</param>
    /// <exception cref="Exceptions.RepositoryException"></exception>
    Task UpdateAsync(TEntity item);

    /// <summary>
    /// Removes entity from the repository.
    /// </summary>
    /// <param name="item"> Entity to be removed.</param>
    /// <exception cref="Exceptions.RepositoryException"></exception>
    Task DeleteAsync(TEntity item);
}