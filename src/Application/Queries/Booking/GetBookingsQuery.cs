using Application.DataTransferObjects.Booking.Requests;
using Application.DataTransferObjects.Booking.Responses;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Application.Queries.Booking;

public class GetListBookingsQuery : IRequest<OffsetPaginationResponse<BookingResponse>>
{
    public required ViewListBookingsRequest Request { get; set; }
    public long? AccountId { get; set; }
}