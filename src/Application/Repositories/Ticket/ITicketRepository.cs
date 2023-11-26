using Application.DataTransferObjects.Ticket.Responses;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Entities;

namespace Application.Repositories.Ticket;
public interface ITicketRepository : IRepository<TicketEntity>
{
    Task<OffsetPaginationResponse<TicketResponse>> GetListTicketsAsync(OffsetPaginationRequest request, CancellationToken cancellationToken);
    Task<TicketResponse?> GetTicketByIdAsync(long id, CancellationToken cancellationToken);
    Task<TicketEntity?> GetTicketEntityByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> IsDuplicatedTicketByNameAndIdAsync(string name, long id, CancellationToken cancellationToken);
    Task<bool> IsDuplicatedTicketByNameAsync(string name, CancellationToken cancellationToken);
}
