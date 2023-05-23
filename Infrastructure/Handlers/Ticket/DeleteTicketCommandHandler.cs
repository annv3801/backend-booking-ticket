using Application.Commands.Ticket;
using Application.Handlers.Ticket;
using Application.Repositories.Ticket;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.Ticket;

public class DeleteTicketCommandHandler : IDeleteTicketCommandHandler
{
    private readonly ITicketRepository _ticketRepository;
    private readonly ApplicationDbContext _applicationDbContext;

    public DeleteTicketCommandHandler(ITicketRepository ticketRepository, ApplicationDbContext applicationDbContext)
    {
        _ticketRepository = ticketRepository;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<int> Handle(DeleteTicketCommand command, CancellationToken cancellationToken)
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