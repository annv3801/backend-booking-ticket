using Application.Commands.Seat;
using Application.Common.Interfaces;
using Application.DataTransferObjects.Seat.Requests;
using Application.DataTransferObjects.Seat.Responses;
using Application.Interface;
using Application.Queries.Seat;
using Application.Repositories.Seat;
using Application.Services.Seat;
using AutoMapper;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Domain.Entities;
using MediatR;
using Nobi.Core.Responses;

// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract


namespace Infrastructure.Services;

public class SeatManagementService : ISeatManagementService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ILoggerService _loggerService;
    private readonly ISeatRepository _seatRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentAccountService _currentAccountService;

    public SeatManagementService(IMapper mapper, IMediator mediator, ILoggerService loggerService, ISeatRepository seatRepository,
        IDateTimeService dateTimeService, ICurrentAccountService currentAccountService)
    {
        _mapper = mapper;
        _mediator = mediator;
        _loggerService = loggerService;
        _seatRepository = seatRepository;
        _dateTimeService = dateTimeService;
        _currentAccountService = currentAccountService;
    }

    public async Task<RequestResult<bool>> CreateSeatAsync(CreateSeatRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // Create Seat 
            var seatEntity = _mapper.Map<SeatEntity>(request);

            seatEntity.CreatedBy = _currentAccountService.Id;
            seatEntity.CreatedTime = _dateTimeService.NowUtc;

            var resultCreateSeat = await _mediator.Send(new CreateSeatCommand {Entity = seatEntity}, cancellationToken);
            if (resultCreateSeat <= 0)
                return RequestResult<bool>.Fail("Save data failed");
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateSeatAsync));
            throw;
        }
    }

    public async Task<RequestResult<bool>> UpdateSeatAsync(UpdateSeatRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var existedSeat = await _seatRepository.GetSeatEntityByIdAsync(request.Id, cancellationToken);
            if (existedSeat == null)
                return RequestResult<bool>.Fail("Seat is not found");

            existedSeat.RoomSeatId = request.RoomSeatId;
            existedSeat.SchedulerId = request.SchedulerId;

            var resultUpdateSeat = await _mediator.Send(new UpdateSeatCommand
            {
                Request = existedSeat,
            }, cancellationToken);
            if (resultUpdateSeat <= 0)
                return RequestResult<bool>.Fail("Save data failed");
           
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(UpdateSeatAsync));
            throw;
        }
    }

    public async Task<RequestResult<bool>> DeleteSeatAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            // Check for existence
            var seatToDelete = await _seatRepository.GetSeatEntityByIdAsync(id, cancellationToken);
            if (seatToDelete == null)
                return RequestResult<bool>.Fail("Seat is not found");


            var resultDeleteSeat = await _mediator.Send(new DeleteSeatCommand
            {
                Id = id,
            }, cancellationToken);
            if (resultDeleteSeat <= 0)
                return RequestResult<bool>.Fail("Save data failed");
           
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(DeleteSeatAsync));
            throw;
        }
    }

    public async Task<RequestResult<SeatResponse>> GetSeatAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var seat = await _mediator.Send(new GetSeatByIdQuery
            {
                Id = id,
            }, cancellationToken);
            if (seat == null)
                return RequestResult<SeatResponse>.Fail("Seat is not found");

            return RequestResult<SeatResponse>.Succeed(null, _mapper.Map<SeatResponse>(seat));
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetSeatAsync));
            throw;
        }
    }

    public async Task<RequestResult<OffsetPaginationResponse<SeatResponse>>> GetListSeatsAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // check tenant valid
            var seat = await _mediator.Send(new GetListSeatsQuery
            {
                OffsetPaginationRequest = request,
            }, cancellationToken);

            return RequestResult<OffsetPaginationResponse<SeatResponse>>.Succeed(null, seat);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetListSeatsAsync));
            throw;
        }
    }

    public async Task<RequestResult<ICollection<SeatResponse>>> GetListSeatsBySchedulerAsync(long schedulerId, CancellationToken cancellationToken)
    {
        try
        {
            // check tenant valid
            var seat = await _mediator.Send(new GetListSeatsBySchedulerQuery
            {
                SchedulerId = schedulerId,
            }, cancellationToken);

            return RequestResult<ICollection<SeatResponse>>.Succeed(null, seat);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetListSeatsBySchedulerAsync));
            throw;
        }
    }
}