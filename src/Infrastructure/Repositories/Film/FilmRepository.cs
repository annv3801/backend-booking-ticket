using Application.DataTransferObjects.Film.Requests;
using Application.Queries.Film;
using Application.Repositories.Film;
using AutoMapper;
using Infrastructure.Common.Repositories;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Film;

public class FilmRepository : Repository<Domain.Entities.Film>, IFilmRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;
    
    public FilmRepository(ApplicationDbContext applicationDbContext, IMapper mapper) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<Domain.Entities.Film?> GetFilmByShortenUrlAsync(string shortenUrl, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Films.FirstOrDefaultAsync(x => x.ShortenUrl == shortenUrl && x.Status != 0, cancellationToken);
    }

    public async Task<Domain.Entities.Film?> GetFilmByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Films.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IQueryable<Domain.Entities.Film>> GetListFilmsAsync(ViewListFilmsRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return _applicationDbContext.Films
            .AsSplitQuery()
            .AsQueryable();
    }

    public async Task<IQueryable<Domain.Entities.Film>> ViewListFilmByCategoryAsync(ViewListFilmByCategoryQuery query, CancellationToken cancellationToken = default(CancellationToken))
    {
        await Task.CompletedTask;
        var category =  await _applicationDbContext.Categories.Where(x => x.ShortenUrl == query.CategorySlug).FirstOrDefaultAsync(cancellationToken);
        return _applicationDbContext.Films.Where(x => x.CategoryId == category.Id)
            .AsSplitQuery()
            .AsQueryable();
    }
}