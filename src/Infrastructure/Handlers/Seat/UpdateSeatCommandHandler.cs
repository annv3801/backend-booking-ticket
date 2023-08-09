using Application.Commands.Seat;
using Application.Handlers.Seat;
using Application.Repositories.Seat;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.Seat;

public class UpdateSeatCommandHandler : IUpdateSeatCommandHandler
{
    private readonly ISeatRepository _seatRepository;
    private readonly ApplicationDbContext _applicationDbContext;
    
    public UpdateSeatCommandHandler(ApplicationDbContext applicationDbContext, ISeatRepository seatRepository)
    {
        _applicationDbContext = applicationDbContext;
        _seatRepository = seatRepository;
    }

    public async Task<int> Handle(UpdateSeatCommand command, CancellationToken cancellationToken)
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