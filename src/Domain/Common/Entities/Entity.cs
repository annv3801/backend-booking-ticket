// ReSharper disable UnusedAutoPropertyAccessor.Global

// ReSharper disable UnusedAutoPropertyAccessor.Local

using Domain.Common.Attributes;

namespace Domain.Common.Entities;

/// <summary>
/// The base class for an Entity
/// </summary>
public abstract class Entity : IEntity
{
}

public abstract class Entity<TKey> : IAuditEntity<TKey>
{
    [Sortable] [Searchable] public virtual TKey Id { get; set; } = default!;
    [Sortable] [Searchable] public DateTimeOffset? CreatedTime { get; set; }
    [Sortable] [Searchable] public long? CreatedBy { get; set; }
    [Sortable] [Searchable] public DateTimeOffset? ModifiedTime { get; set; }
    [Sortable] [Searchable] public long? ModifiedBy { get; set; }
    [Sortable] [Searchable] public bool Deleted { get; set; }
    [Sortable] [Searchable] public long? DeletedBy { get; set; }
    [Sortable] [Searchable] public DateTimeOffset? DeletedTime { get; set; }
}