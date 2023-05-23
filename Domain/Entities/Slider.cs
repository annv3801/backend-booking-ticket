using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class Slider : AuditableEntity
{
    public Guid Id { get; set; }
    public string? Image { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    public int Status { get; set; }
}