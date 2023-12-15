using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common.Attributes;
using Domain.Common.Entities;
using Domain.Constants;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class FilmEntity : Entity<long>
{
    public long Id { get; set; }
    [Searchable] [Sortable]public string Name { get; set; }
    [Searchable] [Sortable]public string Slug { get; set; }
    public long GroupEntityId { get; set; }
    public GroupEntity GroupEntity { get; set; }
    public List<long> CategoryIds { get; set; }
    public string? Description { get; set; }
    public string? Director { get; set; }
    public string? Actor { get; set; }
    public string? Genre { get; set; }
    public string? Premiere { get; set; }
    public double Duration { get; set; }
    public string? Language { get; set; }
    public string? Rated { get; set; }
    public string? Trailer { get; set; }
    public string? Image { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    public decimal TotalRating { get; set; }

    public string Status { get; set; } = EntityStatus.Active;
}
