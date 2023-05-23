using System.Diagnostics.CodeAnalysis;
using Domain.Entities.Identity;

namespace Domain.Common;
[ExcludeFromCodeCoverage]
public abstract class AuditableEntity
{
    public DateTime Created { get; set; }
    public Account? CreatedBy { get; set; }
    public Guid? CreatedById { get; set; }

    public DateTime LastModified { get; set; }
    public Account? LastModifiedBy { get; set; }
    public Guid? LastModifiedById { get; set; }
}
