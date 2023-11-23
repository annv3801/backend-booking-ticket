using System.Diagnostics.CodeAnalysis;
using Domain.Entities;

namespace Application.DataTransferObjects.Film.Responses;
[ExcludeFromCodeCoverage]
public class FilmResponse
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public long Group { get; set; }
    public GroupEntity GroupEntity { get; set; }
    public List<long> CategoryIds { get; set; }
    public string? Description { get; set; }
    public string? Director { get; set; }
    public string? Genre { get; set; }
    public string Actor { get; set; }
    public string Premiere { get; set; }
    public double Duration { get; set; }
    public string Language { get; set; }
    public string Rated { get; set; }
    public string Trailer { get; set; }
    public string Image { get; set; }
    public decimal TotalRating { get; set; } 
    public string Status { get; set; }
    public FeedbackFilmResponse? FeedbackFilmResponse { get; set; }
}



