using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Film.Responses;
using MediatR;

namespace Application.Queries.Film;
[ExcludeFromCodeCoverage]
public class ViewFilmByShortenUrlQuery : IRequest<Result<ViewFilmResponse>>
{
    public string ShortenUrl { get; set; }
}
