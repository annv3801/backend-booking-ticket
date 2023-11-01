using Application.DataTransferObjects.Theater.Responses;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Application.Queries.Theater;

public class GetListTheatersQuery : IRequest<OffsetPaginationResponse<TheaterResponse>>
{
    public required OffsetPaginationRequest OffsetPaginationRequest { get; set; }
}