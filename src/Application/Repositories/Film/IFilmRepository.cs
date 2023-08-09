using Application.Common.Interfaces;
using Application.DataTransferObjects.Film.Requests;
using Application.Queries.Film;

namespace Application.Repositories.Film;

public interface IFilmRepository : IRepository<Domain.Entities.Film>
{
    Task<Domain.Entities.Film?> GetFilmByShortenUrlAsync(string shortenUrl, CancellationToken cancellationToken);
    Task<Domain.Entities.Film?> GetFilmByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IQueryable<Domain.Entities.Film>> GetListFilmsAsync(ViewListFilmsRequest request, CancellationToken cancellationToken);
    Task<IQueryable<Domain.Entities.Film>> ViewListFilmByCategoryAsync(ViewListFilmByCategoryQuery query, CancellationToken cancellationToken = default(CancellationToken));

}