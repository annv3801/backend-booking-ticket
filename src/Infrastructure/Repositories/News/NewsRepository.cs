using Application.DataTransferObjects.News.Requests;
using Application.Repositories.News;
using AutoMapper;
using Infrastructure.Common.Repositories;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.News;

public class NewsRepository : Repository<Domain.Entities.News>, INewsRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;
    
    public NewsRepository(ApplicationDbContext applicationDbContext, IMapper mapper) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<Domain.Entities.News?> GetNewsByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.News.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IQueryable<Domain.Entities.News>> GetListNewsAsync(ViewListNewsRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return _applicationDbContext.News
            .AsSplitQuery()
            .AsQueryable();
    }
    
}