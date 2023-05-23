using Application.Commands.Theater;
using Application.Handlers.Theater;
using Application.Repositories.Theater;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.Theater;

public class CreateTheaterCommandHandler : ICreateTheaterCommandHandler
{
    private readonly ITheaterRepository _theaterRepository;
    private readonly ApplicationDbContext _applicationDbContext;

    public CreateTheaterCommandHandler(ITheaterRepository theaterRepository, ApplicationDbContext applicationDbContext)
    {
        _theaterRepository = theaterRepository;
        _applicationDbContext = applicationDbContext;
    }


    public async Task<int> Handle(CreateTheaterCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _theaterRepository.AddAsync(command.Entity, cancellationToken);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}