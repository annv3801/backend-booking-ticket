using Application.Commands.Booking;
using Application.Common;
using Application.Common.Models;
using MediatR;

namespace Application.Handlers.Booking;

public interface IBookingHandlers : IRequestHandler<BookingCommand, Result<BookingResult>>
{
    
}