using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Pagination.Requests;
using Application.DataTransferObjects.Pagination.Responses;
using Application.DataTransferObjects.Ticket.Responses;
using MediatR;

namespace Application.Queries.Ticket;
[ExcludeFromCodeCoverage]
public class ViewListTicketQuery : PaginationBaseRequest, IRequest<Result<PaginationBaseResponse<ViewTicketResponse>>>
{
}
