using Application.Commands.Seat;
using Application.Handlers.Seat;
using Application.Repositories.Seat;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.Seat;

public class CreateSeatCommandHandler : ICreateSeatCommandHandler
{
    private readonly ISeatRepository _seatRepository;
    private readonly ApplicationDbContext _applicationDbContext;

    public CreateSeatCommandHandler(ISeatRepository seatRepository, ApplicationDbContext applicationDbContext)
    {
        _seatRepository = seatRepository;
        _applicationDbContext = applicationDbContext;
    }


    public async Task<int> Handle(CreateSeatCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _seatRepository.AddAsync(command.Entity, cancellationToken);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}