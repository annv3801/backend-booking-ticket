using Application.Commands.Room;
using Application.Common.Interfaces;
using Application.DataTransferObjects.Room.Requests;
using Application.DataTransferObjects.Room.Responses;
using Application.Interface;
using Application.Queries.Room;
using Application.Repositories.Room;
using Application.Services.Room;
using AutoMapper;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Domain.Entities;
using MediatR;
using Nobi.Core.Responses;

// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract


namespace Infrastructure.Services;

public class RoomManagementService : IRoomManagementService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ILoggerService _loggerService;
    private readonly IRoomRepository _roomRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly ISnowflakeIdService _snowflakeIdService;

    public RoomManagementService(IMapper mapper, IMediator mediator, ILoggerService loggerService, IRoomRepository roomRepository,
        IDateTimeService dateTimeService, ICurrentAccountService currentAccountService, ISnowflakeIdService snowflakeIdService)
    {
        _mapper = mapper;
        _mediator = mediator;
        _loggerService = loggerService;
        _roomRepository = roomRepository;
        _dateTimeService = dateTimeService;
        _currentAccountService = currentAccountService;
        _snowflakeIdService = snowflakeIdService;
    }

    public async Task<RequestResult<bool>> CreateRoomAsync(CreateRoomRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // Check duplicate Room name
            if (await _mediator.Send(new CheckDuplicatedRoomByNameQuery
                {
                    Name = request.Name,
                }, cancellationToken))
                return RequestResult<bool>.Fail("Item is duplicated");

            // Create Room 
            var roomEntity = _mapper.Map<RoomEntity>(request);

            roomEntity.Id = await _snowflakeIdService.GenerateId(cancellationToken);
            roomEntity.CreatedBy = _currentAccountService.Id;
            roomEntity.CreatedTime = _dateTimeService.NowUtc;

            var resultCreateRoom = await _mediator.Send(new CreateRoomCommand {Entity = roomEntity}, cancellationToken);
            if (resultCreateRoom <= 0)
                return RequestResult<bool>.Fail("Save data failed");
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateRoomAsync));
            throw;
        }
    }

    public async Task<RequestResult<bool>> UpdateRoomAsync(UpdateRoomRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // Check duplicate Room name
            if (await _mediator.Send(new CheckDuplicatedRoomByNameAndIdQuery
                {
                    Name = request.Name,
                    Id = request.Id,
                }, cancellationToken))
                return RequestResult<bool>.Fail("Item is duplicated");


            var existedRoom = await _roomRepository.GetRoomEntityByIdAsync(request.Id, cancellationToken);
            if (existedRoom == null)
                return RequestResult<bool>.Fail("Room is not found");

            
            // Update value to existed Room
            existedRoom.Name = request.Name;
            existedRoom.TheaterId = request.TheaterId;

            var resultUpdateRoom = await _mediator.Send(new UpdateRoomCommand
            {
                Request = existedRoom,
            }, cancellationToken);
            if (resultUpdateRoom <= 0)
                return RequestResult<bool>.Fail("Save data failed");
           
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(UpdateRoomAsync));
            throw;
        }
    }

    public async Task<RequestResult<bool>> DeleteRoomAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            // Check for existence
            var roomToDelete = await _roomRepository.GetRoomByIdAsync(id, cancellationToken);
            if (roomToDelete == null)
                return RequestResult<bool>.Fail("Room is not found");


            var resultDeleteRoom = await _mediator.Send(new DeleteRoomCommand
            {
                Id = id,
            }, cancellationToken);
            if (resultDeleteRoom <= 0)
                return RequestResult<bool>.Fail("Save data failed");
           
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(DeleteRoomAsync));
            throw;
        }
    }

    public async Task<RequestResult<RoomResponse>> GetRoomAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var room = await _mediator.Send(new GetRoomByIdQuery
            {
                Id = id,
            }, cancellationToken);
            if (room == null)
                return RequestResult<RoomResponse>.Fail("Room is not found");

            return RequestResult<RoomResponse>.Succeed(null, _mapper.Map<RoomResponse>(room));
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetRoomAsync));
            throw;
        }
    }

    public async Task<RequestResult<OffsetPaginationResponse<RoomResponse>>> GetListRoomsAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // check tenant valid
            var room = await _mediator.Send(new GetListRoomsQuery
            {
                OffsetPaginationRequest = request,
            }, cancellationToken);

            return RequestResult<OffsetPaginationResponse<RoomResponse>>.Succeed(null, room);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetListRoomsAsync));
            throw;
        }
    }

}