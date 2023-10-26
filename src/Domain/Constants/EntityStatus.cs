namespace Domain.Constants;

/// <summary>
/// Manage system's object status
/// </summary>
public class EntityStatus
{
    /// <summary>
    /// Active status
    /// </summary>
    public const string Active = "ACTIVE";
    /// <summary>
    /// Inactivate status
    /// </summary>
    public const string InActive = "INACTIVE";
    /// <summary>
    /// Deleted status
    /// </summary>
    public const string Deleted = "DELETED";
    /// <summary>
    /// Pending due to something status
    /// </summary>
    public const string Pending = "PENDING";
    /// <summary>
    /// Pending for activation status
    /// </summary>
    public const string PendingForActivation = "PENDING_FOR_ACTIVATION";
    /// <summary>
    /// Pending for confirmation status
    /// </summary>
    public const string PendingForConfirmation = "PENDING_FOR_CONFIRMATION";
    /// <summary>
    /// Locked status
    /// </summary>
    public const string Locked = "LOCKED";
    /// <summary>
    /// Pending for approval status
    /// </summary>
    public const string PendingForApproval = "PENDING_FOR_APPROVAL";
    
    /// <summary>
    /// Published status
    /// </summary>
    public const string Published = "PUBLISHED";
    /// <summary>
    /// Draft status
    /// </summary>
    public const string Draft = "DRAFT";
    
    /// <summary>
    /// Scheduled to be published
    /// </summary>
    public const string Scheduled = "SCHEDULED";
    
    
}