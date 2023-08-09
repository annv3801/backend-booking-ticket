using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Ticket.Responses;
using MediatR;

namespace Application.Queries.Ticket;
[ExcludeFromCodeCoverage]
public class ViewTicketByIdQuery : IRequest<Result<ViewTicketResponse>>
{
    public ViewTicketByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
