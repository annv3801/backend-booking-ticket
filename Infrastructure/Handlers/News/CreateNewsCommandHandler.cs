using Application.Commands.News;
using Application.Handlers.News;
using Application.Repositories.News;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.News;

public class CreateNewsCommandHandler : ICreateNewsCommandHandler
{
    private readonly INewsRepository _newsRepository;
    private readonly ApplicationDbContext _applicationDbContext;

    public CreateNewsCommandHandler(INewsRepository newsRepository, ApplicationDbContext applicationDbContext)
    {
        _newsRepository = newsRepository;
        _applicationDbContext = applicationDbContext;
    }


    public async Task<int> Handle(CreateNewsCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _newsRepository.AddAsync(command.Entity, cancellationToken);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}