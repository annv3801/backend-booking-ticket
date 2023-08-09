using Application.Common;
using Application.Common.Models;
using Application.DataTransferObjects.Booking.Requests;
using MediatR;

namespace Application.Commands.Booking;

public class BookingCommand : BookingRequest, IRequest<Result<BookingResult>>
{
    
}