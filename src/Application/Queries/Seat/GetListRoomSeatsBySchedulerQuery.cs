using Application.DataTransferObjects.Seat.Responses;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Application.Queries.Seat;

public class GetListSeatsBySchedulerQuery : IRequest<ICollection<SeatResponse>>
{
    public long SchedulerId { get; set; }
}