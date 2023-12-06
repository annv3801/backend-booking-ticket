using Application.DataTransferObjects.Booking.Responses;
using Application.Queries.Booking;
using Application.Repositories.Booking;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Infrastructure.Handlers.Queries.Booking;

public class BookingQueryHandler :
    IRequestHandler<GetBookingByIdQuery, BookingResponse?>, 
    IRequestHandler<GetListBookingsQuery, OffsetPaginationResponse<BookingResponse>>
{    

    private readonly IBookingRepository _bookingRepository;

    public BookingQueryHandler(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<BookingResponse?> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
    {
        return await _bookingRepository.GetBookingByIdAsync(request.Id, cancellationToken);
    }
    
    public async Task<OffsetPaginationResponse<BookingResponse>> Handle(GetListBookingsQuery request, CancellationToken cancellationToken)
    {
        return await _bookingRepository.GetListBookingsAsync(request.Request, cancellationToken);
    }
}