namespace Domain.Common.Entities;

/// <summary>
/// Track creation action
/// </summary>
public interface ICreationAuditEntity
{
    DateTimeOffset? CreatedTime { get; }

    long? CreatedBy { get; }
}

/// <summary>
/// Track modification action
/// </summary>
public interface IModificationAuditEntity
{
    DateTimeOffset? ModifiedTime { get; }

    long? ModifiedBy { get; }
}

/// <summary>
/// Track deletion action
/// </summary>
public interface IDeletionAuditEntity
{
    bool Deleted { get; }

    long? DeletedBy { get; }

    DateTimeOffset? DeletedTime { get; }
}

/// <summary>
/// Define concurrency field
/// </summary>
public interface IHasConcurrencyTimestamp
{
    public uint ConcurrencyStamp { get; set; }
}

public interface IAuditEntity : ICreationAuditEntity, IModificationAuditEntity, IDeletionAuditEntity, IEntity
{
}

public interface IAuditEntity<TKey> : ICreationAuditEntity, IModificationAuditEntity, IDeletionAuditEntity, IEntity<TKey>
{
}