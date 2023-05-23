using Application.DataTransferObjects.Ticket.Requests;
using Application.Repositories.Ticket;
using AutoMapper;
using Infrastructure.Common.Repositories;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Ticket;

public class TicketRepository : Repository<Domain.Entities.Ticket>, ITicketRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;
    
    public TicketRepository(ApplicationDbContext applicationDbContext, IMapper mapper) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<Domain.Entities.Ticket?> GetTicketByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Tickets.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IQueryable<Domain.Entities.Ticket>> GetListTicketsAsync(ViewListTicketsRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return _applicationDbContext.Tickets
            .AsSplitQuery()
            .AsQueryable();
    }
}