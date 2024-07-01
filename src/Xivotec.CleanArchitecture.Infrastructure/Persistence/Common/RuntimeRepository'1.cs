using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.Exceptions;
using Xivotec.CleanArchitecture.Domain.Common;
using Xivotec.CleanArchitecture.Infrastructure.Persistence.Common.Interfaces;

namespace Xivotec.CleanArchitecture.Infrastructure.Persistence.Common;

/// <summary>
/// Generic repository implementation, normally not used directly.
/// </summary>
/// <typeparam name="TEntity">Entity type of the repository.</typeparam>
public abstract class RuntimeRepository<TEntity>
    : IRepository<TEntity>, IRuntimeRepository where TEntity : Entity
{
    private readonly List<TEntity> _list = [];

    /// <inheritdoc cref="IRepository{TEntity}.AddAsync(TEntity)"/>
    public Task AddAsync(TEntity entity)
    {
        try
        {
            _list.Add(entity);
        }
        catch (Exception ex)
        {
            throw new RepositoryException(ex.Message, ex);
        }
        return Task.CompletedTask;
    }

    /// <inheritdoc cref="IRepository{TEntity}.DeleteAsync(TEntity)"/>
    public async Task DeleteAsync(TEntity entity)
    {
        try
        {
            var toRemove = await GetByIdAsync(entity.Id);
            _list.Remove(toRemove);
        }
        catch (Exception ex)
        {
            throw new RepositoryException(ex.Message, ex);
        }
        await Task.CompletedTask;
    }

    /// <inheritdoc cref="IRepository{TEntity}.GetAllAsync"/>
    public async Task<List<TEntity>> GetAllAsync() => await Task.FromResult(_list);

    /// <inheritdoc cref="IRepository{TEntity}.GetByIdAsync(Guid)"/>
    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        try
        {
            var foundEntities = _list.Where(x => x.Id.Equals(id));
            var entity = foundEntities.Single();

            // Id should be unique --> only one item
            return await Task.FromResult(entity);
        }
        catch (Exception ex)
        {
            throw new ItemNotFoundException(id.ToString(), ex);
        }
    }

    /// <inheritdoc cref="IRepository{TEntity}.UpdateAsync(TEntity)"/>
    public async Task UpdateAsync(TEntity entity)
    {
        try
        {
            var entityToUpdate = await GetByIdAsync(entity.Id);
            var entityIndex = _list.IndexOf(entityToUpdate);
            _list[entityIndex] = entity;
        }
        catch (Exception ex)
        {
            throw new RepositoryException(ex.Message, ex);
        }
    }
}
