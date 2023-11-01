using Application.DataTransferObjects.Film.Requests;
using Application.DataTransferObjects.Film.Responses;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Entities;

namespace Application.Repositories.Film;
public interface IFeedbackFilmRepository : IRepository<FilmFeedbackEntity>
{
    Task<bool> GetFeedbackByAccountIdAndFilmIdAsync(long accountId, long filmId, CancellationToken cancellationToken);
}
