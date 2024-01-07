using Application.DataTransferObjects.News.Requests;
using Application.DataTransferObjects.News.Responses;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Application.Queries.News;

public class GetListNewsQuery : IRequest<OffsetPaginationResponse<NewsResponse>>
{
    public ViewNewsRequest Request { get; set; }
}