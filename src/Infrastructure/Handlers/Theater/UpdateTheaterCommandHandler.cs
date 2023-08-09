using Application.Commands.Theater;
using Application.Handlers.Theater;
using Application.Repositories.Theater;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.Theater;

public class UpdateTheaterCommandHandler : IUpdateTheaterCommandHandler
{
    private readonly ITheaterRepository _theaterRepository;
    private readonly ApplicationDbContext _applicationDbContext;
    
    public UpdateTheaterCommandHandler(ApplicationDbContext applicationDbContext, ITheaterRepository theaterRepository)
    {
        _applicationDbContext = applicationDbContext;
        _theaterRepository = theaterRepository;
    }

    public async Task<int> Handle(UpdateTheaterCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _theaterRepository.Update(command.Entity);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}