using Application.Commands.Booking;
using MediatR;

namespace Application.Handlers.Booking;

public interface ICreateBookingDetailCommandHandler : IRequestHandler<CreateBookingDetailCommand, int>
{
}

