using Application.DataTransferObjects.News.Requests;
using Application.DataTransferObjects.News.Responses;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Entities;

namespace Application.Repositories.News;
public interface INewsRepository : IRepository<NewsEntity>
{
    Task<OffsetPaginationResponse<NewsResponse>> GetListNewsAsync(ViewNewsRequest request, CancellationToken cancellationToken);
    Task<NewsResponse?> GetNewsByIdAsync(long id, CancellationToken cancellationToken);
    Task<NewsEntity?> GetNewsEntityByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> IsDuplicatedNewsByNameAndIdAsync(string name, long id, CancellationToken cancellationToken);
    Task<bool> IsDuplicatedNewsByNameAsync(string name, CancellationToken cancellationToken);
}
