using Domain.Common.Entities;
using Domain.Entities.Identity;

namespace Domain.Entities;

public class AccountCategoryEntity : Entity
{
    public long AccountId { get; set; }
    public Account Account { get; set; }
    public long CategoryId { get; set; }
    public CategoryEntity Category { get; set; }
}