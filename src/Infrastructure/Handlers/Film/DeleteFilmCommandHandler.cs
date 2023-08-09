using Application.Commands.Film;
using Application.Handlers.Film;
using Application.Repositories.Film;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.Film;

public class DeleteFilmCommandHandler : IDeleteFilmCommandHandler
{
    private readonly IFilmRepository _filmRepository;
    private readonly ApplicationDbContext _applicationDbContext;

    public DeleteFilmCommandHandler(IFilmRepository filmRepository, ApplicationDbContext applicationDbContext)
    {
        _filmRepository = filmRepository;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<int> Handle(DeleteFilmCommand command, CancellationToken cancellationToken)
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