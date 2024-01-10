using Domain.Entities;
using MediatR;

namespace Application.Commands.Booking;

public class ChangeStatusBookingCommand : IRequest<int>
{
    public BookingEntity Entity { get; set; }
}