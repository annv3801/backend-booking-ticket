using Application.DataTransferObjects.Seat.Responses;
using Application.Queries.Seat;
using Application.Repositories.Seat;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Infrastructure.Handlers.Queries.Seat;

public class SeatQueryHandler :
    IRequestHandler<GetSeatByIdQuery, SeatResponse?>, 
    IRequestHandler<GetListSeatsQuery, OffsetPaginationResponse<SeatResponse>>
{
    private readonly ISeatRepository _seatRepository;

    public SeatQueryHandler(ISeatRepository seatRepository)
    {
        _seatRepository = seatRepository;
    }

    public async Task<SeatResponse?> Handle(GetSeatByIdQuery request, CancellationToken cancellationToken)
    {
        return await _seatRepository.GetSeatByIdAsync(request.Id, cancellationToken);
    }
    
    public async Task<OffsetPaginationResponse<SeatResponse>> Handle(GetListSeatsQuery request, CancellationToken cancellationToken)
    {
        return await _seatRepository.GetListSeatsAsync(request.OffsetPaginationRequest, cancellationToken);
    }
}