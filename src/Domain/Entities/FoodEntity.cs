using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Common.Entities;
using Domain.Constants;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class FoodEntity : Entity<long>
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string Status { get; set; } = EntityStatus.Active;
    public long GroupEntityId { get; set; }
    public GroupEntity GroupEntity { get; set; }
    public string? Image { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
}