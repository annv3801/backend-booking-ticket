using Application.Commands.Category;
using Application.Handlers.Category;
using Application.Repositories.Category;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.Category;

public class DeleteCategoryCommandHandler : IDeleteCategoryCommandHandler
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ApplicationDbContext _applicationDbContext;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, ApplicationDbContext applicationDbContext)
    {
        _categoryRepository = categoryRepository;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<int> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _categoryRepository.Update(command.Entity);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}