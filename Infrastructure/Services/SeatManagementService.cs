using Application.Commands.Seat;
using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DataTransferObjects.Pagination.Responses;
using Application.DataTransferObjects.Seat.Requests;
using Application.DataTransferObjects.Seat.Responses;
using Application.Queries.Seat;
using Application.Repositories.Seat;
using Application.Services.Seat;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Databases;
using MediatR;

namespace Infrastructure.Services;

public class SeatManagementService : ISeatManagementService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ISeatRepository _seatRepository;
    private readonly IStringLocalizationService _localizationService;
    public readonly ICurrentAccountService CurrentAccountService;
    private readonly IPaginationService _paginationService;
    private readonly ApplicationDbContext _applicationDbContext;

    public SeatManagementService(IMediator mediator, IMapper mapper, ISeatRepository seatRepository, IStringLocalizationService localizationService, ICurrentAccountService currentAccountService, IPaginationService paginationService, ApplicationDbContext applicationDbContext)
    {
        _mediator = mediator;
        _mapper = mapper;
        _seatRepository = seatRepository;
        _localizationService = localizationService;
        CurrentAccountService = currentAccountService;
        _paginationService = paginationService;
        _applicationDbContext = applicationDbContext;
    }
    
    public async Task<Result<SeatResult>> CreateSeatAsync(CreateSeatRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var newField = new Seat()
            {
                Id = new Guid(),
                Name = request.Name,
                ScheduleId = request.ScheduleId,
                Type = request.Type,
                Status = request.Status
            };

            var result = await _mediator.Send(new CreateSeatCommand(newField), cancellationToken);
            return result > 0 ? Result<SeatResult>.Succeed(_mapper.Map<SeatResult>(newField)) : Result<SeatResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<ViewSeatResponse>> ViewSeatAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _seatRepository.GetSeatByIdAsync(id, cancellationToken);
            if (result == null)
                return Result<ViewSeatResponse>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            var seatResult = _mapper.Map<ViewSeatResponse>(result);

            return Result<ViewSeatResponse>.Succeed(seatResult);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<SeatResult>> DeleteSeatAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Check for existence
            var seatToDelete = await _seatRepository.GetSeatByIdAsync(id, cancellationToken);
            if (seatToDelete == null)
                return Result<SeatResult>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            seatToDelete.Status = 0;
            seatToDelete.LastModifiedById = CurrentAccountService.Id;
            seatToDelete.LastModified = DateTime.Now;

            var resultDeleteSeat = await _mediator.Send(new DeleteSeatCommand(seatToDelete), cancellationToken);
            return resultDeleteSeat <= 0 ? Result<SeatResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService)) : Result<SeatResult>.Succeed(_mapper.Map<SeatResult>(seatToDelete));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<PaginationBaseResponse<ViewSeatResponse>>> ViewListSeatsAsync(ViewListSeatsRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var source = await _seatRepository.GetListSeatsAsync(request, cancellationToken);
            var result = await _paginationService.PaginateAsync(source, request.Page, request.OrderBy, request.OrderByDesc, request.Size, cancellationToken);
            if (result.Result.Count == 0)
            {
                return Result<PaginationBaseResponse<ViewSeatResponse>>.Fail(
                    _localizationService[LocalizationString.Category.FailedToViewList].Value.ToErrors(_localizationService));
            }
            return Result<PaginationBaseResponse<ViewSeatResponse>>.Succeed(new PaginationBaseResponse<ViewSeatResponse>()
            {
                CurrentPage = result.CurrentPage,
                OrderBy = result.OrderBy,
                OrderByDesc = result.OrderByDesc,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                Result = result.Result.Select(a => new ViewSeatResponse()
                {
                    Id = a.Id,
                    Name = a.Name,
                    ScheduleId = a.ScheduleId,
                    Type = a.Type,
                    Status = a.Status
                }).ToList()
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<PaginationBaseResponse<ViewSeatResponse>>> ViewListSeatsByScheduleAsync(ViewListSeatByScheduleQuery query, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var filterQuery = await _seatRepository.ViewListSeatsByScheduleAsync(query, cancellationToken);
            var response = filterQuery.Where(x => x.ScheduleId == query.ScheduleId);
            var p1 = response.Select(p => new {p.Name, p.ScheduleId, p.Type, p.Status, p.Id});
            var schedule = _applicationDbContext.FilmSchedules;
            var q1 = p1.Join(schedule, x => x.ScheduleId, y => y.Id, (x, y) => new
            {
                x, y
            });
            var room = _applicationDbContext.Rooms;
            var q2 = q1.Join(room, x => x.y.RoomId, y => y.Id, (x, y) => new
            {
                x, y
            });
            var theater = _applicationDbContext.Theaters;
            var source = q2.Join(theater, x => x.y.TheaterId, y => y.Id, (x, y) => new
            {
                x, y
            });
            var result = await _paginationService.PaginateAsync(source, query.Page, query.OrderBy, query.OrderByDesc, query.Size, cancellationToken);
            if (result.Result.Count == 0)
            {
                return Result<PaginationBaseResponse<ViewSeatResponse>>.Succeed(new PaginationBaseResponse<ViewSeatResponse>()
                {
                    CurrentPage = result.CurrentPage,
                    OrderBy = result.OrderBy,
                    OrderByDesc = result.OrderByDesc,
                    PageSize = result.PageSize,
                    TotalItems = result.TotalItems,
                    TotalPages = result.TotalPages,
                    Result = {}
                });
            }
            return Result<PaginationBaseResponse<ViewSeatResponse>>.Succeed(new PaginationBaseResponse<ViewSeatResponse>()
            {
                CurrentPage = result.CurrentPage,
                OrderBy = result.OrderBy,
                OrderByDesc = result.OrderByDesc,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                Result = result.Result.Select(a => new ViewSeatResponse()
                {
                    Id = a.x.x.x.Id,
                    Name = a.x.x.x.Name,
                    ScheduleId = a.x.x.x.ScheduleId,
                    StartTime = a.x.x.y.StartTime,
                    EndTime = a.x.x.y.EndTime,
                    RoomName = a.x.y.Name,
                    TheaterName = a.y.Name,
                    Type = a.x.x.x.Type,
                    Status = a.x.x.x.Status
                }).ToList()
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}