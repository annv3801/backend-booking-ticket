using Application.Commands.Category;
using Application.Commands.Room;
using Application.Commands.Theater;
using Application.Commands.Ticket;
using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Requests;
using Application.DataTransferObjects.Category.Requests;
using Application.DataTransferObjects.Category.Responses;
using Application.DataTransferObjects.Pagination.Responses;
using Application.DataTransferObjects.Room.Requests;
using Application.DataTransferObjects.Room.Responses;
using Application.DataTransferObjects.Theater.Requests;
using Application.DataTransferObjects.Theater.Responses;
using Application.DataTransferObjects.Ticket.Requests;
using Application.DataTransferObjects.Ticket.Responses;
using Application.Repositories.Category;
using Application.Repositories.Room;
using Application.Repositories.Theater;
using Application.Repositories.Ticket;
using Application.Services.Category;
using Application.Services.Room;
using Application.Services.Theater;
using Application.Services.Ticket;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Databases;
using Infrastructure.Handlers.Room;
using MediatR;

namespace Infrastructure.Services;

public class RoomManagementService : IRoomManagementService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IRoomRepository _theaterRepository;
    private readonly IStringLocalizationService _localizationService;
    public readonly ICurrentAccountService CurrentAccountService;
    private readonly IPaginationService _paginationService;
    private readonly ApplicationDbContext _applicationDbContext;

    public RoomManagementService(IMediator mediator, IMapper mapper, IRoomRepository theaterRepository, IStringLocalizationService localizationService, ICurrentAccountService currentAccountService, IPaginationService paginationService, ApplicationDbContext applicationDbContext)
    {
        _mediator = mediator;
        _mapper = mapper;
        _theaterRepository = theaterRepository;
        _localizationService = localizationService;
        CurrentAccountService = currentAccountService;
        _paginationService = paginationService;
        _applicationDbContext = applicationDbContext;
    }
    
    public async Task<Result<RoomResult>> CreateRoomAsync(CreateRoomRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var newField = new Room()
            {
                Id = new Guid(),
                Name = request.Name,
                TheaterId = request.TheaterId,
                Status = request.Status
            };

            var result = await _mediator.Send(new CreateRoomCommand(newField), cancellationToken);
            return result > 0 ? Result<RoomResult>.Succeed(_mapper.Map<RoomResult>(newField)) : Result<RoomResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<ViewRoomResponse>> ViewRoomAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _theaterRepository.GetRoomByIdAsync(id, cancellationToken);
            if (result == null)
                return Result<ViewRoomResponse>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            var theaterResult = _mapper.Map<ViewRoomResponse>(result);

            return Result<ViewRoomResponse>.Succeed(theaterResult);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<RoomResult>> DeleteRoomAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Check for existence
            var roomToDelete = await _theaterRepository.GetRoomByIdAsync(id, cancellationToken);
            if (roomToDelete == null)
                return Result<RoomResult>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            roomToDelete.Status = 0;
            roomToDelete.LastModifiedById = CurrentAccountService.Id;
            roomToDelete.LastModified = DateTime.Now;

            var resultDeleteRoom = await _mediator.Send(new DeleteRoomCommand(roomToDelete), cancellationToken);
            return resultDeleteRoom <= 0 ? Result<RoomResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService)) : Result<RoomResult>.Succeed(_mapper.Map<RoomResult>(roomToDelete));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<RoomResult>> UpdateRoomAsync(Guid id, UpdateRoomRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find Theater
            var existedRoom = await _theaterRepository.GetRoomByIdAsync(id, cancellationToken);
            if (existedRoom == null)
                return Result<RoomResult>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            existedRoom.Name = request.Name;
            existedRoom.TheaterId = request.TheaterId;
            existedRoom.Status = request.Status;
            existedRoom.LastModified = DateTime.Now;
            existedRoom.LastModifiedById = CurrentAccountService.Id;

            var resultUpdateRoom = await _mediator.Send(new UpdateRoomCommand(existedRoom), cancellationToken);
            return resultUpdateRoom <= 0 ? Result<RoomResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService)) : Result<RoomResult>.Succeed(_mapper.Map<RoomResult>(existedRoom));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<PaginationBaseResponse<ViewRoomResponse>>> ViewListRoomsAsync(ViewListRoomsRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var source = await _theaterRepository.GetListRoomsAsync(request, cancellationToken);
            var theater = _applicationDbContext.Theaters;
            var source1 = source.Join(theater, x => x.TheaterId, y => y.Id, (x, y) => new
            {
                x,
                y
            });
            var result = await _paginationService.PaginateAsync(source1, request.Page, request.OrderBy, request.OrderByDesc, request.Size, cancellationToken);
            if (result.Result.Count == 0)
            {
                return Result<PaginationBaseResponse<ViewRoomResponse>>.Fail(
                    _localizationService[LocalizationString.Category.FailedToViewList].Value.ToErrors(_localizationService));
            }
            return Result<PaginationBaseResponse<ViewRoomResponse>>.Succeed(new PaginationBaseResponse<ViewRoomResponse>()
            {
                CurrentPage = result.CurrentPage,
                OrderBy = result.OrderBy,
                OrderByDesc = result.OrderByDesc,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                Result = result.Result.Select(a => new ViewRoomResponse()
                {
                    Id = a.x.Id,
                    Name = a.x.Name,
                    TheaterId = a.x.TheaterId,
                    TheaterName = a.y.Name,
                    TheaterAddress = a.y.Address,
                    Status = a.x.Status
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