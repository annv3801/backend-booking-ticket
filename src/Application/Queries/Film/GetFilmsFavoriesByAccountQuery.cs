using Application.DataTransferObjects.Film.Requests;
using Application.DataTransferObjects.Film.Responses;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Application.Queries.Film;

public class GetFilmsFavoriesByAccountQuery : IRequest<OffsetPaginationResponse<FilmResponse>>
{
    public ViewListFilmsFavoriteByAccountRequest Request { get; set; }
    public long AccontId { get; set; }
}