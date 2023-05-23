using Application.Commands.FilmSchedules;
using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DataTransferObjects.Pagination.Responses;
using Application.DataTransferObjects.FilmSchedules.Requests;
using Application.DataTransferObjects.FilmSchedules.Responses;
using Application.Queries.FilmSchedules;
using Application.Repositories.FilmSchedules;
using Application.Services.FilmSchedules;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Databases;
using MediatR;

namespace Infrastructure.Services;

public class FilmScheduleManagementService : IFilmSchedulesManagementService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IFilmSchedulesRepository _filmScheduleRepository;
    private readonly IStringLocalizationService _localizationService;
    public readonly ICurrentAccountService CurrentAccountService;
    private readonly IPaginationService _paginationService;
    private readonly ApplicationDbContext _applicationDbContext;

    public FilmScheduleManagementService(IMediator mediator, IMapper mapper, IFilmSchedulesRepository filmScheduleRepository, IStringLocalizationService localizationService, ICurrentAccountService currentAccountService, IPaginationService paginationService, ApplicationDbContext applicationDbContext)
    {
        _mediator = mediator;
        _mapper = mapper;
        _filmScheduleRepository = filmScheduleRepository;
        _localizationService = localizationService;
        CurrentAccountService = currentAccountService;
        _paginationService = paginationService;
        _applicationDbContext = applicationDbContext;
    }
    
    public async Task<Result<FilmSchedulesResult>> CreateFilmSchedulesAsync(CreateFilmSchedulesRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var newField = new FilmSchedule()
            {
                Id = new Guid(),
                FilmId = request.FilmId,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                RoomId = request.RoomId,
                Status = request.Status
            };

            var result = await _mediator.Send(new CreateFilmSchedulesCommand(newField), cancellationToken);
            return result > 0 ? Result<FilmSchedulesResult>.Succeed(_mapper.Map<FilmSchedulesResult>(newField)) : Result<FilmSchedulesResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<ViewFilmSchedulesResponse>> ViewFilmSchedulesAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _filmScheduleRepository.GetFilmSchedulesByIdAsync(id, cancellationToken);
            if (result == null)
                return Result<ViewFilmSchedulesResponse>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            var filmScheduleResult = _mapper.Map<ViewFilmSchedulesResponse>(result);

            return Result<ViewFilmSchedulesResponse>.Succeed(filmScheduleResult);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<FilmSchedulesResult>> DeleteFilmSchedulesAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Check for existence
            var filmScheduleToDelete = await _filmScheduleRepository.GetFilmSchedulesByIdAsync(id, cancellationToken);
            if (filmScheduleToDelete == null)
                return Result<FilmSchedulesResult>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            filmScheduleToDelete.Status = 0;
            filmScheduleToDelete.LastModifiedById = CurrentAccountService.Id;
            filmScheduleToDelete.LastModified = DateTime.Now;

            var resultDeleteFilmSchedule = await _mediator.Send(new DeleteFilmSchedulesCommand(filmScheduleToDelete), cancellationToken);
            return resultDeleteFilmSchedule <= 0 ? Result<FilmSchedulesResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService)) : Result<FilmSchedulesResult>.Succeed(_mapper.Map<FilmSchedulesResult>(filmScheduleToDelete));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<FilmSchedulesResult>> UpdateFilmSchedulesAsync(Guid id, UpdateFilmSchedulesRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find role
            var existedFilm = await _filmScheduleRepository.GetFilmSchedulesByIdAsync(id, cancellationToken);
            if (existedFilm == null)
                return Result<FilmSchedulesResult>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            existedFilm.StartTime = request.StartTime;
            existedFilm.EndTime = request.EndTime;
            existedFilm.LastModified = DateTime.Now;
            existedFilm.LastModifiedById = CurrentAccountService.Id;

            var resultUpdateFilm = await _mediator.Send(new UpdateFilmSchedulesCommand(existedFilm), cancellationToken);
            return resultUpdateFilm <= 0 ? Result<FilmSchedulesResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService)) : Result<FilmSchedulesResult>.Succeed(_mapper.Map<FilmSchedulesResult>(existedFilm));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<PaginationBaseResponse<ViewFilmSchedulesResponse>>> ViewListFilmSchedulesAsync(ViewListFilmSchedulesRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var source = await _filmScheduleRepository.GetListFilmSchedulesAsync(request, cancellationToken);
            var film = _applicationDbContext.Films;
            var filmJoinSchedule = source.Join(film, x => x.FilmId, y => y.Id, (x, y) => new
            {
                x, y
            });
            var room = _applicationDbContext.Rooms;
            var roomJoin = filmJoinSchedule.Join(room, x => x.x.RoomId, y => y.Id, (x, y) => new
            {
                x, y
            });
            var theater = _applicationDbContext.Theaters;
            var source1 = roomJoin.Join(theater, x => x.y.TheaterId, y => y.Id, (x, y) => new
            {
                x, y
            });
            var result = await _paginationService.PaginateAsync(source1, request.Page, request.OrderBy, request.OrderByDesc, request.Size, cancellationToken);
            if (result.Result.Count == 0)
            {
                return Result<PaginationBaseResponse<ViewFilmSchedulesResponse>>.Fail(
                    _localizationService[LocalizationString.Category.FailedToViewList].Value.ToErrors(_localizationService));
            }
            return Result<PaginationBaseResponse<ViewFilmSchedulesResponse>>.Succeed(new PaginationBaseResponse<ViewFilmSchedulesResponse>()
            {
                CurrentPage = result.CurrentPage,
                OrderBy = result.OrderBy,
                OrderByDesc = result.OrderByDesc,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                Result = result.Result.Select(a => new ViewFilmSchedulesResponse()
                {
                    Id = a.x.x.x.Id,
                    RoomId = a.x.x.x.RoomId,
                    RoomName = a.x.y.Name,
                    TheaterName = a.y.Name,
                    FilmId = a.x.x.x.FilmId,
                    FilmName = a.x.x.y.Name,
                    StartTime = a.x.x.x.StartTime,
                    EndTime = a.x.x.x.EndTime,
                    Status = a.x.x.y.Status
                }).ToList()
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Result<List<TheaterScheduleResponse>>> ViewListFilmSchedulesByTimeAsync(ViewListFilmSchedulesByTimeQuery query, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var filterQuery = await _filmScheduleRepository.ViewListFilmSchedulesByTimeAsync(query, cancellationToken);
            var response = filterQuery.Where(p => p.StartTime.Date == query.Date.Date && p.FilmId == query.FilmId);
            var getListSchedules = response.Select(p => new {p.RoomId, p.FilmId, p.StartTime, p.Id, p.EndTime});
            var film = _applicationDbContext.Films;
            var filmJoinSchedule = getListSchedules.Join(film, x => x.FilmId, y => y.Id, (x, y) => new
            {
                x, y
            });
            var room = _applicationDbContext.Rooms;
            var roomJoin = filmJoinSchedule.Join(room, x => x.x.RoomId, y => y.Id, (x, y) => new
            {
                x, y
            });
            var theater = _applicationDbContext.Theaters;
            var source = roomJoin.Join(theater, x => x.y.TheaterId, y => y.Id, (x, y) => new
                {
                    TheaterId = y.Id,
                    TheaterName = y.Name,
                    Schedule = new ScheduleResponse
                    {
                        Id = x.x.x.Id,
                        StartTime = x.x.x.StartTime,
                        EndTime = x.x.x.EndTime,
                        FilmId = x.x.y.Id,
                        FilmName = x.x.y.Name
                    }
                })
                .GroupBy(x => new { x.TheaterId, x.TheaterName })
                .Select(g => new TheaterScheduleResponse
                {
                    TheaterId = g.Key.TheaterId,
                    TheaterName = g.Key.TheaterName,
                    ListSchedule = g.Select(x => x.Schedule).ToList()
                });
            return Result<List<TheaterScheduleResponse>>.Succeed(source.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}