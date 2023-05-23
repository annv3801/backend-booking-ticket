using Application.Common.Interfaces;
using Application.DataTransferObjects.FilmSchedules.Requests;
using Application.Queries.FilmSchedules;
using Domain.Entities;

namespace Application.Repositories.FilmSchedules;

public interface IFilmSchedulesRepository : IRepository<Domain.Entities.FilmSchedule>
{
    Task<IQueryable<FilmSchedule>> GetListFilmSchedulesAsync(ViewListFilmSchedulesRequest request, CancellationToken cancellationToken);
    Task<FilmSchedule?> GetFilmSchedulesByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IQueryable<FilmSchedule>> ViewListFilmSchedulesByTimeAsync(ViewListFilmSchedulesByTimeQuery request, CancellationToken cancellationToken = default(CancellationToken));

}