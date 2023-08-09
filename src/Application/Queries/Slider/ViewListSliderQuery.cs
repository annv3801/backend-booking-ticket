using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Pagination.Requests;
using Application.DataTransferObjects.Pagination.Responses;
using Application.DataTransferObjects.Slider.Responses;
using MediatR;

namespace Application.Queries.Slider;
[ExcludeFromCodeCoverage]
public class ViewListSliderQuery : PaginationBaseRequest, IRequest<Result<PaginationBaseResponse<ViewSliderResponse>>>
{
}
