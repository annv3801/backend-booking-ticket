using System.Diagnostics.CodeAnalysis;
using Application.DataTransferObjects.Booking.Requests;
using MediatR;

namespace Application.Commands.Booking;
[ExcludeFromCodeCoverage]
public class UpdateReceivedBookingCommand : UpdateReceivedBookingRequest, IRequest<int>
{
    public UpdateReceivedBookingCommand(Domain.Entities.Booking entity)
    {
        Entity = entity;
    }

    public Domain.Entities.Booking Entity { get; set; }
}
