using Application.Commands.Ticket;
using Application.Handlers.Ticket;
using Application.Repositories.Ticket;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.Ticket;

public class UpdateTicketCommandHandler : IUpdateTicketCommandHandler
{
    private readonly ITicketRepository _ticketRepository;
    private readonly ApplicationDbContext _applicationDbContext;
    
    public UpdateTicketCommandHandler(ApplicationDbContext applicationDbContext, ITicketRepository ticketRepository)
    {
        _applicationDbContext = applicationDbContext;
        _ticketRepository = ticketRepository;
    }

    public async Task<int> Handle(UpdateTicketCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _ticketRepository.Update(command.Entity);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}