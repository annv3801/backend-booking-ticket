using Application.DataTransferObjects.Seat.Responses;
using MediatR;

namespace Application.Queries.Seat;

public class GetSeatByIdQuery : IRequest<SeatResponse?>
{
    public long Id { get; set; }
}