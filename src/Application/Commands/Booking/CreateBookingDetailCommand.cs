using Domain.Entities;
using MediatR;

namespace Application.Commands.Booking;

public class CreateBookingDetailCommand : IRequest<int>
{
    public BookingDetailEntity Entity { get; set; }
}