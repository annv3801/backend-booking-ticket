using Domain.Common;
using Domain.Common.Attributes;
using Domain.Common.Entities;
using Domain.Constants;

namespace Domain.Entities;

public class CategoryEntity : Entity<long>
{
    public long Id { get; set; }
    [Searchable]public string Name { get; set; }
    public string Status { get; set; } = EntityStatus.Active;
}