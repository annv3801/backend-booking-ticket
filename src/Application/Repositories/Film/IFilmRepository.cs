using Application.DataTransferObjects.Film.Requests;
using Application.DataTransferObjects.Film.Responses;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Entities;

namespace Application.Repositories.Film;
public interface IFilmRepository : IRepository<FilmEntity>
{
    Task<OffsetPaginationResponse<FilmResponse>> GetListFilmsByGroupsAsync(ViewListFilmsByGroupRequest request, long? accountId, CancellationToken cancellationToken);
    Task<OffsetPaginationResponse<FilmResponse>> GetListFilmsFavoritesByAccountAsync(ViewListFilmsFavoriteByAccountRequest request, long accountId, CancellationToken cancellationToken);
    Task<OffsetPaginationResponse<FilmResponse>> GetListFilmsAsync(OffsetPaginationRequest request, CancellationToken cancellationToken);
    Task<FilmResponse?> GetFilmByIdAsync(long id, long? accountId, CancellationToken cancellationToken);
    Task<FilmEntity?> GetFilmEntityByIdAsync(long id, CancellationToken cancellationToken);
    // Task<bool> IsDuplicatedFilmByNameAndIdAsync(string name, long id, CancellationToken cancellationToken);
    // Task<bool> IsDuplicatedFilmByNameAsync(string name, CancellationToken cancellationToken);
    Task<FilmResponse?> GetFilmBySlugAsync(string slug, CancellationToken cancellationToken);
}
