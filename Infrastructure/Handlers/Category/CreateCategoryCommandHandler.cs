using Application.Commands.Category;
using Application.Common;
using Application.Common.Models;
using Application.Handlers.Category;
using Application.Repositories.Category;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.Category;

public class CreateCategoryCommandHandler : ICreateCategoryCommandHandler
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ApplicationDbContext _applicationDbContext;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, ApplicationDbContext applicationDbContext)
    {
        _categoryRepository = categoryRepository;
        _applicationDbContext = applicationDbContext;
    }


    public async Task<int> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _categoryRepository.AddAsync(command.Entity, cancellationToken);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}