using Domain.Entities;
using MediatR;

namespace Application.Commands.Booking;

public class CancelBookingCommand : IRequest<int>
{
    public BookingEntity Entity { get; set; }
}