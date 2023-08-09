using Application.Commands.Booking;
using MediatR;

namespace Application.Handlers.Booking;

public interface IUpdateReceivedBookingCommandHandler : IRequestHandler<UpdateReceivedBookingCommand, int>
{
}