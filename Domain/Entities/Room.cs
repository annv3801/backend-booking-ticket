using Domain.Common;

namespace Domain.Entities;

public class Room : AuditableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int? Status { get; set; }
    public Guid TheaterId { get; set; }
    public Theater Theater { get; set; }
}