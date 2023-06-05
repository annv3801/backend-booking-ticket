using Application.DataTransferObjects.FilmSchedules.Requests;
using Application.DataTransferObjects.Room.Requests;
using Application.Queries.FilmSchedules;
using Application.Repositories.FilmSchedules;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Common.Repositories;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.FilmSchedules;

public class FilmSchedulesRepository : Repository<Domain.Entities.FilmSchedule>, IFilmSchedulesRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;
    
    public FilmSchedulesRepository(ApplicationDbContext applicationDbContext, IMapper mapper) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<FilmSchedule?> GetFilmSchedulesByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.FilmSchedules.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IQueryable<FilmSchedule>> ViewListFilmSchedulesByTimeAsync(ViewListFilmSchedulesByTimeQuery request, CancellationToken cancellationToken = default(CancellationToken))
    {
        await Task.CompletedTask;
        return _applicationDbContext.FilmSchedules.Where(x => x.StartTime <= DateTime.UtcNow)
            .AsSplitQuery()
            .AsQueryable();
    }

    public async Task<IQueryable<FilmSchedule>> GetListFilmSchedulesAsync(ViewListFilmSchedulesRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return _applicationDbContext.FilmSchedules
            .AsSplitQuery()
            .AsQueryable();
    }
    
}