using Application.Commands.FilmSchedules;
using Application.Handlers.FilmSchedules;
using Application.Repositories.FilmSchedules;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.FilmSchedules;

public class DeleteFilmSchedulesCommandHandler : IDeleteFilmSchedulesCommandHandler
{
    private readonly IFilmSchedulesRepository _filmSchedulesRepository;
    private readonly ApplicationDbContext _applicationDbContext;

    public DeleteFilmSchedulesCommandHandler(IFilmSchedulesRepository filmSchedulesRepository, ApplicationDbContext applicationDbContext)
    {
        _filmSchedulesRepository = filmSchedulesRepository;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<int> Handle(DeleteFilmSchedulesCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _filmSchedulesRepository.Update(command.Entity);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}