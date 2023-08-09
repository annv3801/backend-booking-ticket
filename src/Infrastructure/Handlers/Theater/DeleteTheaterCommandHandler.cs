using Application.Commands.Theater;
using Application.Handlers.Theater;
using Application.Repositories.Theater;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.Theater;

public class DeleteTheaterCommandHandler : IDeleteTheaterCommandHandler
{
    private readonly ITheaterRepository _theaterRepository;
    private readonly ApplicationDbContext _applicationDbContext;

    public DeleteTheaterCommandHandler(ITheaterRepository theaterRepository, ApplicationDbContext applicationDbContext)
    {
        _theaterRepository = theaterRepository;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<int> Handle(DeleteTheaterCommand command, CancellationToken cancellationToken)
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