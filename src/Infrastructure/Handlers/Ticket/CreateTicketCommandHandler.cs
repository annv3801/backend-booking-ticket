using Application.Commands.Ticket;
using Application.Handlers.Ticket;
using Application.Repositories.Ticket;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.Ticket;

public class CreateTicketCommandHandler : ICreateTicketCommandHandler
{
    private readonly ITicketRepository _ticketRepository;
    private readonly ApplicationDbContext _applicationDbContext;

    public CreateTicketCommandHandler(ITicketRepository ticketRepository, ApplicationDbContext applicationDbContext)
    {
        _ticketRepository = ticketRepository;
        _applicationDbContext = applicationDbContext;
    }


    public async Task<int> Handle(CreateTicketCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _ticketRepository.AddAsync(command.Entity, cancellationToken);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}