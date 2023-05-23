using Application.Commands.FilmSchedules;
using Application.Handlers.FilmSchedules;
using Application.Repositories.FilmSchedules;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.FilmSchedules;

public class UpdateFilmSchedulesCommandHandler : IUpdateFilmSchedulesCommandHandler
{
    private readonly IFilmSchedulesRepository _filmSchedulesRepository;
    private readonly ApplicationDbContext _applicationDbContext;
    
    public UpdateFilmSchedulesCommandHandler(ApplicationDbContext applicationDbContext, IFilmSchedulesRepository filmSchedulesRepository)
    {
        _applicationDbContext = applicationDbContext;
        _filmSchedulesRepository = filmSchedulesRepository;
    }

    public async Task<int> Handle(UpdateFilmSchedulesCommand command, CancellationToken cancellationToken)
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