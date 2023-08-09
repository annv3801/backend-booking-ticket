using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.FilmSchedules.Responses;
using Application.DataTransferObjects.Pagination.Requests;
using Application.DataTransferObjects.Pagination.Responses;
using MediatR;

namespace Application.Queries.FilmSchedules;
[ExcludeFromCodeCoverage]
public class ViewListFilmSchedulesQuery : PaginationBaseRequest, IRequest<Result<PaginationBaseResponse<ViewFilmSchedulesResponse>>>
{
}
