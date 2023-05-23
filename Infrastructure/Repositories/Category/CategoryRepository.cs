using Application.DataTransferObjects.Category.Requests;
using Application.Repositories.Account;
using Application.Repositories.Category;
using AutoMapper;
using Infrastructure.Common.Repositories;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Category;

public class CategoryRepository : Repository<Domain.Entities.Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;
    
    public CategoryRepository(ApplicationDbContext applicationDbContext, IMapper mapper) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<Domain.Entities.Category?> GetCategoryByShortenUrlAsync(string shortenUrl, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Categories.FirstOrDefaultAsync(x => x.ShortenUrl == shortenUrl && x.Status != 0, cancellationToken);
    }

    public async Task<Domain.Entities.Category?> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IQueryable<Domain.Entities.Category>> GetListCategoryAsync(ViewListCategoriesRequest requestViewFieldWithPaginationRequest, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return _applicationDbContext.Categories
            .AsSplitQuery()
            .AsQueryable();
    }
}