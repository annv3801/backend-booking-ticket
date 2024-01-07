using Application.DataTransferObjects.News.Requests;
using Application.DataTransferObjects.News.Responses;
using Domain.Common.Pagination.OffsetBased;
using Nobi.Core.Responses;

namespace Application.Services.News;

public interface INewsManagementService
{
    Task<RequestResult<bool>> CreateNewsAsync(CreateNewsRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> UpdateNewsAsync(UpdateNewsRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> DeleteNewsAsync(long id, CancellationToken cancellationToken);
    Task<RequestResult<NewsResponse>> GetNewsAsync(long id, CancellationToken cancellationToken);
    Task<RequestResult<OffsetPaginationResponse<NewsResponse>>> GetListNewsAsync(ViewNewsRequest request, CancellationToken cancellationToken);
}