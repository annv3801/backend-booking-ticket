using Application.Common.Interfaces;
using Application.DataTransferObjects.News.Requests;

namespace Application.Repositories.News;

public interface INewsRepository : IRepository<Domain.Entities.News>
{
    Task<IQueryable<Domain.Entities.News>> GetListNewsAsync(ViewListNewsRequest request, CancellationToken cancellationToken);
    Task<Domain.Entities.News?> GetNewsByIdAsync(Guid id, CancellationToken cancellationToken);
}