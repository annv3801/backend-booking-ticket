using Application.DataTransferObjects.RoomSeat.Responses;
using Application.Queries.RoomSeat;
using Application.Repositories.RoomSeat;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Infrastructure.Handlers.Queries.RoomSeat;

public class RoomSeatQueryHandler :
    IRequestHandler<GetRoomSeatByIdQuery, RoomSeatResponse?>, 
    IRequestHandler<GetListRoomSeatsQuery, OffsetPaginationResponse<RoomSeatResponse>>,
    IRequestHandler<GetListRoomSeatsByRoomQuery, ICollection<RoomSeatResponse>>,
    IRequestHandler<CheckDuplicatedRoomSeatByNameAndIdQuery, bool>,
    IRequestHandler<CheckDuplicatedRoomSeatByNameQuery, bool>
{
    private readonly IRoomSeatRepository _roomSeatRepository;

    public RoomSeatQueryHandler(IRoomSeatRepository roomSeatRepository)
    {
        _roomSeatRepository = roomSeatRepository;
    }

    public async Task<ICollection<RoomSeatResponse>> Handle(GetListRoomSeatsByRoomQuery request, CancellationToken cancellationToken)
    {
        return await _roomSeatRepository.GetListRoomSeatsByRoomAsync(request.RoomId, cancellationToken);
    }
    
    public async Task<RoomSeatResponse?> Handle(GetRoomSeatByIdQuery request, CancellationToken cancellationToken)
    {
        return await _roomSeatRepository.GetRoomSeatByIdAsync(request.Id, cancellationToken);
    }
    
    public async Task<OffsetPaginationResponse<RoomSeatResponse>> Handle(GetListRoomSeatsQuery request, CancellationToken cancellationToken)
    {
        return await _roomSeatRepository.GetListRoomSeatsAsync(request.OffsetPaginationRequest, cancellationToken);
    }
    
    public async Task<bool> Handle(CheckDuplicatedRoomSeatByNameAndIdQuery request, CancellationToken cancellationToken)
    {
        return await _roomSeatRepository.IsDuplicatedRoomSeatByNameAndIdAsync(request.Name, request.Id, request.RoomId, cancellationToken);
    }
    
    public async Task<bool> Handle(CheckDuplicatedRoomSeatByNameQuery request, CancellationToken cancellationToken)
    {
        return await _roomSeatRepository.IsDuplicatedRoomSeatByNameAsync(request.Name, request.RoomId, cancellationToken);
    }
}