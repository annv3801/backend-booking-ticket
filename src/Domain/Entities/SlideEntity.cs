using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Common.Entities;
using Domain.Constants;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class SlideEntity : Entity<long>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Image { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    public long ObjectId { get; set; }
    public string Status { get; set; } = EntityStatus.Active;
}