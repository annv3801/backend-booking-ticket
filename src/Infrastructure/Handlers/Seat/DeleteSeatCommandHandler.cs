using Application.Commands.Seat;
using Application.Handlers.Seat;
using Application.Repositories.Seat;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.Seat;

public class DeleteSeatCommandHandler : IDeleteSeatCommandHandler
{
    private readonly ISeatRepository _seatRepository;
    private readonly ApplicationDbContext _applicationDbContext;

    public DeleteSeatCommandHandler(ISeatRepository seatRepository, ApplicationDbContext applicationDbContext)
    {
        _seatRepository = seatRepository;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<int> Handle(DeleteSeatCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _seatRepository.Update(command.Entity);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}