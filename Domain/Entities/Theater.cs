using Domain.Common;

namespace Domain.Entities;

public class Theater : AuditableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int Status { get; set; }
}