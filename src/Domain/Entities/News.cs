using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class News : AuditableEntity
{
    public Guid Id { get; set; }
    public string CategoryId { get; set; }
    public string? Image { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
}