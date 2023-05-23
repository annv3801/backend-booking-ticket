using Application.Commands.Category;
using Application.Handlers.Category;
using Application.Repositories.Category;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.Category;

public class UpdateCategoryCommandHandler : IUpdateCategoryCommandHandler
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ApplicationDbContext _applicationDbContext;
    
    public UpdateCategoryCommandHandler(ApplicationDbContext applicationDbContext, ICategoryRepository categoryRepository)
    {
        _applicationDbContext = applicationDbContext;
        _categoryRepository = categoryRepository;
    }

    public async Task<int> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
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