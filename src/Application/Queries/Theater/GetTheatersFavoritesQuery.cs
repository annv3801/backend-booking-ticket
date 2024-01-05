using Application.DataTransferObjects.Theater.Requests;
using Application.DataTransferObjects.Theater.Responses;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Application.Queries.Theater;

public class GetTheatersFavoritesQuery : IRequest<OffsetPaginationResponse<TheaterResponse>>
{
    public ViewTheaterFavoriteRequest Request { get; set; }
    public long AccountId { get; set; }
}