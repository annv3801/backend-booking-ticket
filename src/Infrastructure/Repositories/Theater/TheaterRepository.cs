using Application.DataTransferObjects.Theater.Requests;
using Application.Repositories.Theater;
using AutoMapper;
using Infrastructure.Common.Repositories;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Theater;

public class TheaterRepository : Repository<Domain.Entities.Theater>, ITheaterRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;
    
    public TheaterRepository(ApplicationDbContext applicationDbContext, IMapper mapper) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<Domain.Entities.Theater?> GetTheaterByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Theaters.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IQueryable<Domain.Entities.Theater>> GetListTheatersAsync(ViewListTheatersRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return _applicationDbContext.Theaters
            .AsSplitQuery()
            .AsQueryable();
    }
}