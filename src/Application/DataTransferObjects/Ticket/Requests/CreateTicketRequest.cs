// ReSharper disable UnusedAutoPropertyAccessor.Global

using Domain.Constants;

namespace Application.DataTransferObjects.Ticket.Requests;

public class CreateTicketRequest
{
    public required string Title { get; set; }
    public int Type { get; set; }
    public long Price { get; set; }
}