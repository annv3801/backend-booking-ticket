using Domain.Entities;
using MediatR;

namespace Application.Commands.Booking;

public class CreateBookingCommand : IRequest<int>
{
    public required BookingEntity Entity { get; set; }
}