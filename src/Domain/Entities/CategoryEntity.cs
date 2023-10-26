using Domain.Common;
using Domain.Common.Entities;
using Domain.Constants;

namespace Domain.Entities;

public class CategoryEntity : Entity<long>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; } = EntityStatus.Active;
}