using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Film.Responses;
using Application.DataTransferObjects.Pagination.Requests;
using Application.DataTransferObjects.Pagination.Responses;
using MediatR;

namespace Application.Queries.Film;
[ExcludeFromCodeCoverage]
public class ViewListFilmsQuery : PaginationBaseRequest, IRequest<Result<PaginationBaseResponse<ViewFilmResponse>>>
{
}
