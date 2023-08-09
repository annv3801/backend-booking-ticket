using Application.Commands.Film;
using Application.Handlers.Film;
using Application.Repositories.Film;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.Film;

public class UpdateFilmCommandHandler : IUpdateFilmCommandHandler
{
    private readonly IFilmRepository _filmRepository;
    private readonly ApplicationDbContext _applicationDbContext;
    
    public UpdateFilmCommandHandler(ApplicationDbContext applicationDbContext, IFilmRepository filmRepository)
    {
        _applicationDbContext = applicationDbContext;
        _filmRepository = filmRepository;
    }

    public async Task<int> Handle(UpdateFilmCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _filmRepository.Update(command.Entity);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}