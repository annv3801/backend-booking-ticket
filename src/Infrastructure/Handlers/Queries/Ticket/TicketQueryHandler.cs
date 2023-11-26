using Application.DataTransferObjects.Ticket.Responses;
using Application.Queries.Ticket;
using Application.Repositories.Ticket;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Infrastructure.Handlers.Queries.Ticket;

public class TicketQueryHandler :
    IRequestHandler<GetTicketByIdQuery, TicketResponse?>, 
    IRequestHandler<GetListTicketsQuery, OffsetPaginationResponse<TicketResponse>>,
    IRequestHandler<CheckDuplicatedTicketByNameAndIdQuery, bool>,
    IRequestHandler<CheckDuplicatedTicketByNameQuery, bool>
{
    private readonly ITicketRepository _ticketRepository;

    public TicketQueryHandler(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<TicketResponse?> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
    {
        return await _ticketRepository.GetTicketByIdAsync(request.Id, cancellationToken);
    }
    
    public async Task<OffsetPaginationResponse<TicketResponse>> Handle(GetListTicketsQuery request, CancellationToken cancellationToken)
    {
        return await _ticketRepository.GetListTicketsAsync(request.OffsetPaginationRequest, cancellationToken);
    }
    
    public async Task<bool> Handle(CheckDuplicatedTicketByNameAndIdQuery request, CancellationToken cancellationToken)
    {
        return await _ticketRepository.IsDuplicatedTicketByNameAndIdAsync(request.Title, request.Id, cancellationToken);
    }
    
    public async Task<bool> Handle(CheckDuplicatedTicketByNameQuery request, CancellationToken cancellationToken)
    {
        return await _ticketRepository.IsDuplicatedTicketByNameAsync(request.Title, cancellationToken);
    }
}