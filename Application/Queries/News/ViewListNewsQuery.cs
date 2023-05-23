using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.News.Responses;
using Application.DataTransferObjects.Pagination.Requests;
using Application.DataTransferObjects.Pagination.Responses;
using MediatR;

namespace Application.Queries.News;
[ExcludeFromCodeCoverage]
public class ViewListNewsQuery : PaginationBaseRequest, IRequest<Result<PaginationBaseResponse<ViewNewsResponse>>>
{
}
