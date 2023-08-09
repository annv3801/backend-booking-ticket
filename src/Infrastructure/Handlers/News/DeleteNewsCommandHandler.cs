using Application.Commands.News;
using Application.Handlers.News;
using Application.Repositories.News;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.News;

public class DeleteNewsCommandHandler : IDeleteNewsCommandHandler
{
    private readonly INewsRepository _newsRepository;
    private readonly ApplicationDbContext _applicationDbContext;

    public DeleteNewsCommandHandler(INewsRepository newsRepository, ApplicationDbContext applicationDbContext)
    {
        _newsRepository = newsRepository;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<int> Handle(DeleteNewsCommand command, CancellationToken cancellationToken)
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