using Domain.Common;
using Domain.Common.Entities;
using Domain.Constants;

namespace Domain.Entities;

public class GroupEntity : Entity<long>
{
    public long Id { get; set; }
    public string Title { get; set; }
    public int Index { get; set; }
    public string Status { get; set; } = EntityStatus.Active;
}