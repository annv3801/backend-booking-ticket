using Application.Common;
using Application.Common.Models;
using Application.DataTransferObjects.News.Requests;
using Application.DataTransferObjects.News.Responses;
using Application.DataTransferObjects.Pagination.Responses;

namespace Application.Services.News;
public interface INewsManagementService
{
    Task<Result<NewsResult>> CreateNewsAsync(CreateNewsRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewNewsResponse>> ViewNewsAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<NewsResult>> DeleteNewsAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<NewsResult>> UpdateNewsAsync(Guid id, UpdateNewsRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<PaginationBaseResponse<ViewNewsResponse>>> ViewListNewsAsync(ViewListNewsRequest request, CancellationToken cancellationToken = default(CancellationToken));

}
