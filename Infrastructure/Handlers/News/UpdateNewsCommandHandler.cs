using Application.Commands.News;
using Application.Handlers.News;
using Application.Repositories.News;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.News;

public class UpdateNewsCommandHandler : IUpdateNewsCommandHandler
{
    private readonly INewsRepository _newsRepository;
    private readonly ApplicationDbContext _applicationDbContext;
    
    public UpdateNewsCommandHandler(ApplicationDbContext applicationDbContext, INewsRepository newsRepository)
    {
        _applicationDbContext = applicationDbContext;
        _newsRepository = newsRepository;
    }

    public async Task<int> Handle(UpdateNewsCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _newsRepository.Update(command.Entity);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}