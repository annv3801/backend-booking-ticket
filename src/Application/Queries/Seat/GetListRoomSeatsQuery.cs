using Application.DataTransferObjects.Seat.Responses;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Application.Queries.Seat;

public class GetListSeatsQuery : IRequest<OffsetPaginationResponse<SeatResponse>>
{
    public OffsetPaginationRequest OffsetPaginationRequest { get; set; }
}