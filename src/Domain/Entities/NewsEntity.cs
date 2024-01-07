using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common.Entities;
using Domain.Constants;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class NewsEntity : Entity<long>
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? Image { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    public long GroupEntityId { get; set; }
    public GroupEntity GroupEntity { get; set; }
    public string Status { get; set; } = EntityStatus.Active;
}