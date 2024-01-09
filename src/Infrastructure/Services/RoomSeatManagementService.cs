﻿using Application.Commands.RoomSeat;
using Application.Common.Interfaces;
using Application.DataTransferObjects.RoomSeat.Requests;
using Application.DataTransferObjects.RoomSeat.Responses;
using Application.Interface;
using Application.Queries.RoomSeat;
using Application.Repositories.RoomSeat;
using Application.Services.RoomSeat;
using AutoMapper;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Domain.Entities;
using MediatR;
using Nobi.Core.Responses;

// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract


namespace Infrastructure.Services;

public class RoomSeatManagementService : IRoomSeatManagementService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ILoggerService _loggerService;
    private readonly IRoomSeatRepository _roomSeatRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentAccountService _currentAccountService;

    public RoomSeatManagementService(IMapper mapper, IMediator mediator, ILoggerService loggerService, IRoomSeatRepository roomSeatRepository,
        IDateTimeService dateTimeService, ICurrentAccountService currentAccountService)
    {
        _mapper = mapper;
        _mediator = mediator;
        _loggerService = loggerService;
        _roomSeatRepository = roomSeatRepository;
        _dateTimeService = dateTimeService;
        _currentAccountService = currentAccountService;
    }

    public async Task<RequestResult<bool>> CreateRoomSeatAsync(CreateRoomSeatRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // Check duplicate RoomSeat name
            foreach (var item in request.RoomSeat)
            {
                if (await _mediator.Send(new CheckDuplicatedRoomSeatByNameQuery
                    {
                        Name = item.Name,
                        RoomId = item.RoomId
                    }, cancellationToken))
                    return RequestResult<bool>.Fail("Item is duplicated");
            } 
            
            // Create RoomSeat 
            var roomSeatEntities = _mapper.Map<List<RoomSeatEntity>>(request.RoomSeat);

            foreach (var entity in roomSeatEntities)
            {
                entity.CreatedBy = _currentAccountService.Id;
                entity.CreatedTime = _dateTimeService.NowUtc;
            }

            var resultCreateRoomSeat = await _mediator.Send(new CreateRoomSeatCommand {Entity = roomSeatEntities}, cancellationToken);
            if (resultCreateRoomSeat <= 0)
                return RequestResult<bool>.Fail("Save data failed");
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateRoomSeatAsync));
            throw;
        }
    }

    public async Task<RequestResult<bool>> UpdateRoomSeatAsync(UpdateRoomSeatRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // Check duplicate RoomSeat name
            if (await _mediator.Send(new CheckDuplicatedRoomSeatByNameAndIdQuery
                {
                    Name = request.Name,
                    Id = request.Id,
                    RoomId = request.RoomId
                }, cancellationToken))
                return RequestResult<bool>.Fail("Item is duplicated");

            var existedRoomSeat = await _roomSeatRepository.GetRoomSeatEntityByIdAsync(request.Id, cancellationToken);
            if (existedRoomSeat == null)
                return RequestResult<bool>.Fail("RoomSeat is not found");
            
            // Update value to existed RoomSeat
            existedRoomSeat.Name = request.Name;
            existedRoomSeat.RoomId = request.RoomId;

            var resultUpdateRoomSeat = await _mediator.Send(new UpdateRoomSeatCommand
            {
                Request = existedRoomSeat,
            }, cancellationToken);
            if (resultUpdateRoomSeat <= 0)
                return RequestResult<bool>.Fail("Save data failed");
           
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(UpdateRoomSeatAsync));
            throw;
        }
    }

    public async Task<RequestResult<bool>> DeleteRoomSeatAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            // Check for existence
            var roomSeatToDelete = await _roomSeatRepository.GetRoomSeatByIdAsync(id, cancellationToken);
            if (roomSeatToDelete == null)
                return RequestResult<bool>.Fail("RoomSeat is not found");


            var resultDeleteRoomSeat = await _mediator.Send(new DeleteRoomSeatCommand
            {
                Id = id,
            }, cancellationToken);
            if (resultDeleteRoomSeat <= 0)
                return RequestResult<bool>.Fail("Save data failed");
           
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(DeleteRoomSeatAsync));
            throw;
        }
    }

    public async Task<RequestResult<RoomSeatResponse>> GetRoomSeatAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var roomSeat = await _mediator.Send(new GetRoomSeatByIdQuery
            {
                Id = id,
            }, cancellationToken);
            if (roomSeat == null)
                return RequestResult<RoomSeatResponse>.Fail("RoomSeat is not found");

            return RequestResult<RoomSeatResponse>.Succeed(null, _mapper.Map<RoomSeatResponse>(roomSeat));
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetRoomSeatAsync));
            throw;
        }
    }

    public async Task<RequestResult<OffsetPaginationResponse<RoomSeatResponse>>> GetListRoomSeatsAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // check tenant valid
            var roomSeat = await _mediator.Send(new GetListRoomSeatsQuery
            {
                OffsetPaginationRequest = request,
            }, cancellationToken);

            return RequestResult<OffsetPaginationResponse<RoomSeatResponse>>.Succeed(null, roomSeat);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetListRoomSeatsAsync));
            throw;
        }
    }

    public async Task<RequestResult<ICollection<RoomSeatResponse>>> GetListRoomSeatsByRoomAsync(long roomId, CancellationToken cancellationToken)
    {
        try
        {
            // check tenant valid
            var roomSeat = await _mediator.Send(new GetListRoomSeatsByRoomQuery()
            {
                RoomId = roomId,
            }, cancellationToken);

            return RequestResult<ICollection<RoomSeatResponse>>.Succeed(null, roomSeat);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetListRoomSeatsAsync));
            throw;
        }
    }
}