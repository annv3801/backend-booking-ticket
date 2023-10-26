using System.Collections.ObjectModel;
using Application.Interface;
using Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Common.Repository;

public class Repository<TEntity, TDbContext> : IRepository<TEntity> where TEntity : class, IEntity where TDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    private readonly TDbContext _dbContext;
    private readonly ISnowflakeIdService _snowflakeIdService;
    public DbSet<TEntity> Entity { get; set; }

    public Repository(TDbContext context, ISnowflakeIdService snowflakeIdService)
    {
        _snowflakeIdService = snowflakeIdService;
        _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        Entity = _dbContext.Set<TEntity>();
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entityEntry = await Entity.AddAsync(entity, cancellationToken);
        return entityEntry.Entity;
    }

    public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        var result = new Collection<TEntity>();
        foreach (var entity in entities)
            result.Add(await AddAsync(entity, cancellationToken));
        return result;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entityEntry = Entity.Update(entity);
        return await Task.FromResult(entityEntry.Entity);
    }

    public async Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        var result = new Collection<TEntity>();
        foreach (var entity in entities)
            result.Add(await UpdateAsync(entity, cancellationToken));
        return result;
    }

    public async Task<TEntity> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entityEntry = Entity.Remove(entity);
        return await Task.FromResult(entityEntry.Entity);
    }

    public async Task<IEnumerable<TEntity>> RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        var result = new Collection<TEntity>();
        foreach (var entity in entities)
            result.Add(await RemoveAsync(entity, cancellationToken));
        return result;
    }

    public IQueryable<TEntity> Queryable()
    {
        return _dbContext.Set<TEntity>();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }

    #region Private methods

    #endregion
}