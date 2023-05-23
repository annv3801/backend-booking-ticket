using Application.Commands.Film;
using Application.Handlers.Film;
using Application.Repositories.Film;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.Film;

public class CreateFilmCommandHandler : ICreateFilmCommandHandler
{
    private readonly IFilmRepository _filmRepository;
    private readonly ApplicationDbContext _applicationDbContext;

    public CreateFilmCommandHandler(IFilmRepository filmRepository, ApplicationDbContext applicationDbContext)
    {
        _filmRepository = filmRepository;
        _applicationDbContext = applicationDbContext;
    }


    public async Task<int> Handle(CreateFilmCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _filmRepository.AddAsync(command.Entity, cancellationToken);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}