using System.Diagnostics.CodeAnalysis;
using Application.DataTransferObjects.Film.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.DataTransferObjects.Film.Requests;
[ExcludeFromCodeCoverage]
public class UpdateFilmRequest
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public long GroupId { get; set; }
    public List<long> CategoryIds { get; set; }
    public string? Description { get; set; }
    public string? Director { get; set; }
    public string? Actor { get; set; }
    public string? Premiere { get; set; }
    public double Duration { get; set; }
    public string? Language { get; set; }
    public string? Rated { get; set; }
    public string? Trailer { get; set; }
    public IFormFile? Image { get; set; }
}
