using Application.DataTransferObjects.Film.Requests;
using Application.DataTransferObjects.Film.Responses;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Application.Queries.Film;

public class GetListFilmsByGroupQuery : IRequest<OffsetPaginationResponse<FilmResponse>>
{
    public ViewListFilmsByGroupRequest Request { get; set; }
}