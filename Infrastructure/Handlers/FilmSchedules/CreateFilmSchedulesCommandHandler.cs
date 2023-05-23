using Application.Commands.FilmSchedules;
using Application.Handlers.FilmSchedules;
using Application.Repositories.FilmSchedules;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.FilmSchedules;

public class CreateFilmSchedulesCommandHandler : ICreateFilmSchedulesCommandHandler
{
    private readonly IFilmSchedulesRepository _filmSchedulesRepository;
    private readonly ApplicationDbContext _applicationDbContext;

    public CreateFilmSchedulesCommandHandler(IFilmSchedulesRepository filmSchedulesRepository, ApplicationDbContext applicationDbContext)
    {
        _filmSchedulesRepository = filmSchedulesRepository;
        _applicationDbContext = applicationDbContext;
    }


    public async Task<int> Handle(CreateFilmSchedulesCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _filmSchedulesRepository.AddAsync(command.Entity, cancellationToken);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}