using Application.DataTransferObjects.Room.Responses;
using Application.Queries.Room;
using Application.Repositories.Room;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Infrastructure.Handlers.Queries.Room;

public class RoomQueryHandler :
    IRequestHandler<GetRoomByIdQuery, RoomResponse?>, 
    IRequestHandler<GetListRoomsQuery, OffsetPaginationResponse<RoomResponse>>,
    IRequestHandler<CheckDuplicatedRoomByNameAndIdQuery, bool>,
    IRequestHandler<CheckDuplicatedRoomByNameQuery, bool>
{
    private readonly IRoomRepository _roomRepository;

    public RoomQueryHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<RoomResponse?> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        return await _roomRepository.GetRoomByIdAsync(request.Id, cancellationToken);
    }
    
    public async Task<OffsetPaginationResponse<RoomResponse>> Handle(GetListRoomsQuery request, CancellationToken cancellationToken)
    {
        return await _roomRepository.GetListRoomsAsync(request.OffsetPaginationRequest, cancellationToken);
    }
    public async Task<bool> Handle(CheckDuplicatedRoomByNameAndIdQuery request, CancellationToken cancellationToken)
    {
        return await _roomRepository.IsDuplicatedRoomByNameAndIdAsync(request.Name, request.Id, cancellationToken);
    }
    
    public async Task<bool> Handle(CheckDuplicatedRoomByNameQuery request, CancellationToken cancellationToken)
    {
        return await _roomRepository.IsDuplicatedRoomByNameAsync(request.Name, cancellationToken);
    }
}