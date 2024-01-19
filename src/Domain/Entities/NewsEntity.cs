using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common.Attributes;
using Domain.Common.Entities;
using Domain.Constants;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class NewsEntity : Entity<long>
{
    public long Id { get; set; }
    [Searchable]public string Title { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public string? Image { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    public string Status { get; set; } = EntityStatus.Active;
}