using Application.Commands.Booking;
using MediatR;

namespace Application.Handlers.Booking;

public interface ICreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, int>
{
}

public interface ICancelBookingCommandHandler : IRequestHandler<CancelBookingCommand, int>
{
}

