using Application.DataTransferObjects.Booking.Responses;
using MediatR;

namespace Application.Queries.Booking;

public class GetBookingByIdQuery : IRequest<BookingResponse?>
{
    public long Id { get; set; }
}