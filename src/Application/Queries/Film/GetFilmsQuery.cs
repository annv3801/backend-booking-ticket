using Application.DataTransferObjects.Film.Requests;
using Application.DataTransferObjects.Film.Responses;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Application.Queries.Film;

public class GetListFilmsQuery : IRequest<OffsetPaginationResponse<FilmResponse>>
{
    public required ViewListFilmsByGroupRequest Request { get; set; }
}