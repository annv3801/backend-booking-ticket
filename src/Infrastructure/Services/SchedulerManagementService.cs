using Application.Commands.Scheduler;
using Application.Common.Interfaces;
using Application.DataTransferObjects.Scheduler.Requests;
using Application.DataTransferObjects.Scheduler.Responses;
using Application.Interface;
using Application.Queries.Scheduler;
using Application.Repositories.Film;
using Application.Repositories.Room;
using Application.Repositories.Scheduler;
using Application.Repositories.Theater;
using Application.Services.Scheduler;
using AutoMapper;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Domain.Entities;
using MediatR;
using Nobi.Core.Responses;

// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract


namespace Infrastructure.Services;

public class SchedulerManagementService : ISchedulerManagementService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ILoggerService _loggerService;
    private readonly ISchedulerRepository _schedulerRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly ISnowflakeIdService _snowflakeIdService;
    private readonly IRoomRepository _roomRepository;
    private readonly IFilmRepository _filmRepository;
    private readonly ITheaterRepository _theaterRepository;

    public SchedulerManagementService(IMapper mapper, IMediator mediator, ILoggerService loggerService, ISchedulerRepository schedulerRepository,
        IDateTimeService dateTimeService, ICurrentAccountService currentAccountService, ISnowflakeIdService snowflakeIdService, IRoomRepository roomRepository, IFilmRepository filmRepository, ITheaterRepository theaterRepository)
    {
        _mapper = mapper;
        _mediator = mediator;
        _loggerService = loggerService;
        _schedulerRepository = schedulerRepository;
        _dateTimeService = dateTimeService;
        _currentAccountService = currentAccountService;
        _snowflakeIdService = snowflakeIdService;
        _roomRepository = roomRepository;
        _filmRepository = filmRepository;
        _theaterRepository = theaterRepository;
    }

    public async Task<RequestResult<bool>> CreateSchedulerAsync(CreateSchedulerRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // Check valid room
            var roomValid = await _roomRepository.GetRoomByIdAsync(request.RoomId, cancellationToken);
            if (roomValid == null)
                return RequestResult<bool>.Fail("Room is not found");
            
            // Check valid film
            var filmValid = await _filmRepository.GetFilmByIdAsync(request.FilmId, null, cancellationToken);
            if (filmValid == null)
                return RequestResult<bool>.Fail("Film is not found");

            var filmValidTime = await _schedulerRepository.GetSchedulerByTime(request.RoomId, request.StartTime, cancellationToken);
            if (filmValidTime != null)
                return RequestResult<bool>.Fail("Start time is not found");

            // Create Scheduler 
            var schedulerEntity = _mapper.Map<SchedulerEntity>(request);

            schedulerEntity.Id = await _snowflakeIdService.GenerateId(cancellationToken);
            schedulerEntity.EndTime = schedulerEntity.StartTime.AddMinutes(filmValid.Duration);
            schedulerEntity.CreatedBy = _currentAccountService.Id;
            schedulerEntity.CreatedTime = _dateTimeService.NowUtc;

            var resultCreateScheduler = await _mediator.Send(new CreateSchedulerCommand {Entity = schedulerEntity}, cancellationToken);
            if (resultCreateScheduler <= 0)
                return RequestResult<bool>.Fail("Save data failed");
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateSchedulerAsync));
            throw;
        }
    }

    public async Task<RequestResult<bool>> UpdateSchedulerAsync(UpdateSchedulerRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var existedScheduler = await _schedulerRepository.GetSchedulerEntityByIdAsync(request.Id, cancellationToken);
            if (existedScheduler == null)
                return RequestResult<bool>.Fail("Scheduler is not found");

            // Check valid room
            var roomValid = await _roomRepository.GetRoomByIdAsync(request.RoomId, cancellationToken);
            if (roomValid != null)
                return RequestResult<bool>.Fail("Room is not found");
            
            // Check valid film
            var filmValid = await _filmRepository.GetFilmByIdAsync(request.FilmId, null, cancellationToken);
            if (filmValid != null)
                return RequestResult<bool>.Fail("Film is not found");

            var filmValidTime = await _schedulerRepository.GetSchedulerByTime(request.RoomId, request.StartTime, cancellationToken);
            if (filmValidTime != null)
                return RequestResult<bool>.Fail("Start time is not found");
            
            // Update value to existed Scheduler
            existedScheduler.FilmId = request.FilmId;
            existedScheduler.RoomId = request.RoomId;
            existedScheduler.StartTime = request.StartTime;
            existedScheduler.EndTime = request.EndTime;

            var resultUpdateScheduler = await _mediator.Send(new UpdateSchedulerCommand
            {
                Request = existedScheduler,
            }, cancellationToken);
            if (resultUpdateScheduler <= 0)
                return RequestResult<bool>.Fail("Save data failed");
           
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(UpdateSchedulerAsync));
            throw;
        }
    }

    public async Task<RequestResult<bool>> DeleteSchedulerAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            // Check for existence
            var schedulerToDelete = await _schedulerRepository.GetSchedulerByIdAsync(id, cancellationToken);
            if (schedulerToDelete == null)
                return RequestResult<bool>.Fail("Scheduler is not found");


            var resultDeleteScheduler = await _mediator.Send(new DeleteSchedulerCommand
            {
                Id = id,
            }, cancellationToken);
            if (resultDeleteScheduler <= 0)
                return RequestResult<bool>.Fail("Save data failed");
           
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(DeleteSchedulerAsync));
            throw;
        }
    }

    public async Task<RequestResult<SchedulerResponse>> GetSchedulerAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var scheduler = await _mediator.Send(new GetSchedulerByIdQuery
            {
                Id = id,
            }, cancellationToken);
            if (scheduler == null)
                return RequestResult<SchedulerResponse>.Fail("Scheduler is not found");

            return RequestResult<SchedulerResponse>.Succeed(null, _mapper.Map<SchedulerResponse>(scheduler));
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetSchedulerAsync));
            throw;
        }
    }

    public async Task<RequestResult<ICollection<SchedulerGroupResponse>>> GetSchedulerByTheaterIdAndDateAsync(long theaterId, string date, CancellationToken cancellationToken)
    {
        try
        {
            var schedulerTheater = await _schedulerRepository.GetSchedulerByDateAndTheaterIdAsync(theaterId, date, cancellationToken);
            return RequestResult<ICollection<SchedulerGroupResponse>>.Succeed(null, schedulerTheater);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetListSchedulersAsync));
            throw;
        }
    }
    
    public async Task<RequestResult<ICollection<SchedulerGroupResponse>>> GetSchedulerByTheaterIdAndDateAndFilmIdAsync(long theaterId, string date, long filmId, CancellationToken cancellationToken)
    {
        try
        {
            var schedulerTheater = await _schedulerRepository.GetSchedulerByDateAndTheaterIdAndFilmIdAsync(theaterId, date, filmId, cancellationToken);
            return RequestResult<ICollection<SchedulerGroupResponse>>.Succeed(null, schedulerTheater);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetSchedulerByTheaterIdAndDateAndFilmIdAsync));
            throw;
        }
    }

    public async Task<RequestResult<ICollection<SchedulerGroupResponse>>> GetSchedulerByDateAndFilmIdAsync(string date, long filmId, CancellationToken cancellationToken)
    {
        try
        {
            var schedulerTheater = await _schedulerRepository.GetSchedulerByDateAndFilmIdAsync(date, filmId, cancellationToken);
            return RequestResult<ICollection<SchedulerGroupResponse>>.Succeed(null, schedulerTheater);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetSchedulerByDateAndFilmIdAsync));
            throw;
        }
    }

    public async Task<RequestResult<OffsetPaginationResponse<SchedulerResponse>>> GetListSchedulersAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // check tenant valid
            var scheduler = await _mediator.Send(new GetListSchedulersQuery
            {
                OffsetPaginationRequest = request,
            }, cancellationToken);

            return RequestResult<OffsetPaginationResponse<SchedulerResponse>>.Succeed(null, scheduler);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetListSchedulersAsync));
            throw;
        }
    }

    public async Task<RequestResult<OffsetPaginationResponse<SchedulerFilmAndTheaterResponse>>> GetListTheaterByFilmAsync(OffsetPaginationRequest request, long filmId, CancellationToken cancellationToken)
    {
        try
        {
            // check tenant valid
            var scheduler = await _mediator.Send(new GetTheatersByFilmQuery()
            {
                OffsetPaginationRequest = request,
                FilmId = filmId
            }, cancellationToken);

            return RequestResult<OffsetPaginationResponse<SchedulerFilmAndTheaterResponse>>.Succeed(null, scheduler);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetListTheaterByFilmAsync));
            throw;
        }
    }
}