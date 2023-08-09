using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.Film.Requests;
[ExcludeFromCodeCoverage]
public class UpdateFilmRequest
{
    public string Name { get; set; }
    public string ShortenUrl { get; set; }
    public Guid CategoryId { get; set; }
    public string? Description { get; set; }
    public string? Director { get; set; }
    public string? Genre { get; set; }
    public string? Actor { get; set; }
    public string? Premiere { get; set; }
    public string? Duration { get; set; }
    public string? Language { get; set; }
    public string? Rated { get; set; }
    public int Status { get; set; }
    public string Trailer { get; set; }
}
