using Application.Common.Interfaces;
using Application.DataTransferObjects.Ticket.Requests;

namespace Application.Repositories.Ticket;

public interface ITicketRepository : IRepository<Domain.Entities.Ticket>
{
    Task<Domain.Entities.Ticket?> GetTicketByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IQueryable<Domain.Entities.Ticket>> GetListTicketsAsync(ViewListTicketsRequest request, CancellationToken cancellationToken);
}