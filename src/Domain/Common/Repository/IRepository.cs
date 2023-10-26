using Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Common.Repository;

public interface IRepository<TEntity> : IDisposable, IEntity where TEntity : class
{
    DbSet<TEntity> Entity { get; set; }

    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

    Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

    Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

    Task<TEntity> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

    Task<IEnumerable<TEntity>> RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

    IQueryable<TEntity> Queryable();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
}