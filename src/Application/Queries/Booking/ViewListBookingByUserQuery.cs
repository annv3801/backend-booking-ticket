using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Booking.Responses;
using Application.DataTransferObjects.Category.Responses;
using Application.DataTransferObjects.Pagination.Requests;
using Application.DataTransferObjects.Pagination.Responses;
using MediatR;

namespace Application.Queries.Booking;
[ExcludeFromCodeCoverage]
public class ViewListBookingByUserQuery : PaginationBaseRequest, IRequest<Result<PaginationBaseResponse<BookingResponse>>>
{
}
