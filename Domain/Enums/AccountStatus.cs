namespace Domain.Enums;
public enum AccountStatus
{
    Locked = -2,
    Deleted = -1,
    Inactive = 0,
    PendingConfirmation = 1,
    PendingApproval = 2,
    Active = 3,
    
}
