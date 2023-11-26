using System.Diagnostics.CodeAnalysis;
using Domain.Constants;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Application.DataTransferObjects.Ticket.Requests;

[ExcludeFromCodeCoverage]
public class UpdateTicketRequest
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public int Type { get; set; }
    public required string Price { get; set; }
}