using Application.Commands.Booking;
using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Responses;
using Application.DataTransferObjects.Booking.Requests;
using Application.DataTransferObjects.Booking.Responses;
using Application.DataTransferObjects.Pagination.Responses;
using Application.DataTransferObjects.Seat.Responses;
using Application.Logging.ActionLog.Services;
using Application.Repositories.Account;
using Application.Repositories.Booking;
using Application.Repositories.Category;
using Application.Repositories.Seat;
using Application.Services.Booking;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Databases;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;
public class BookingManagementService : IBookingManagementService
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IEmailService _emailService;
    private readonly ISeatRepository _seatRepository;
    private readonly IVnPayService _vnPayService;
    private readonly IStringLocalizationService _localizationService;
    private readonly IPaginationService _paginationService;
    private readonly IBookingRepository _bookingRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    public BookingManagementService(IActionLogService actionLogService, IStringLocalizationService localizationService, IJsonSerializerService jsonSerializerService, IMapper mapper, IPaginationService paginationService, ApplicationDbContext applicationDbContext, IMediator mediator, IEmailService emailService, ISeatRepository seatRepository, IVnPayService vnPayService, IBookingRepository bookingRepository, IAccountRepository accountRepository, ICurrentAccountService currentAccountService)
    {
        _localizationService = localizationService;
        _mapper = mapper;
        _paginationService = paginationService;
        _applicationDbContext = applicationDbContext;
        _mediator = mediator;
        _emailService = emailService;
        _seatRepository = seatRepository;
        _vnPayService = vnPayService;
        _bookingRepository = bookingRepository;
        _accountRepository = accountRepository;
        _currentAccountService = currentAccountService;
    }
    
    public async Task<Result<BookingResult>> BookingAsync(BookingRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var seatNames = new List<string>();
            foreach (var seatId in request.SeatId)
            {
                var seat = await _seatRepository.GetSeatByIdAsync(seatId, cancellationToken);
                seatNames.Add(seat.Name);
            }
            
            var filmSchedule = _applicationDbContext.FilmSchedules.Where(x => x.Id == request.ScheduleId);
            var room = _applicationDbContext.Rooms;
            var p1 = filmSchedule.Join(room, x => x.RoomId, y => y.Id, (x, y) => new
            {
                x, y
            });
            var film = _applicationDbContext.Films;
            var p2 = p1.Join(film, x => x.x.FilmId, y => y.Id, (x, y) => new
            {
                x, y
            });
            var theater = _applicationDbContext.Theaters;
            var p3 = p2.Join(theater, x => x.x.y.TheaterId, y => y.Id, (x, y) => new
            {
                x, y   
            }).FirstOrDefault();
            var tableRows = "";
            for (int i = 0; i < seatNames.Count; i++)
            {
                var tableRow = seatNames[i];
                tableRows += tableRow;
                if (i < seatNames.Count - 1)
                    tableRows += ", ";
            }

            var body = $@"Tên phim: {p3.x.y.Name}Thời gian bắt đầu: {p3.x.x.x.StartTime}Tên ghế: {tableRows}";
            _emailService.SendEmail("nva030801@gmail.com", "Test", body);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (result > 0)
            {
                return Result<BookingResult>.Succeed();
            }
            return Result<BookingResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<PaginationBaseResponse<BookingResponse>>> ViewListBookingsByUserAsync(ViewListBookingByUserRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var accountId = _currentAccountService.Id;
            var existedAccount = await _accountRepository.ViewMyAccountAsync(accountId, cancellationToken);
            var source = await _bookingRepository.GetListBookingByUserAsync(request, cancellationToken);
            var listResult = new List<BookingResponse>();
            var result = await _paginationService.PaginateAsync(source, request.Page, request.OrderBy, request.OrderByDesc, request.Size, cancellationToken);
            foreach (var item in result.Result)
            {
                var listSeatName = new List<BookingDetailResponse>();
                var listSeatId = await _bookingRepository.GetListBookingDetailAsync(item.Id, cancellationToken);
                foreach (var item1 in listSeatId)
                {
                    var existedName = await _seatRepository.GetSeatByIdAsync(item1.SeatId, cancellationToken);
                    listSeatName.Add(new BookingDetailResponse()
                    {
                        SeatName = existedName.Name
                    });
                }
                string seatNamesString = string.Join(", ", listSeatName.Select(x => x.SeatName));
                listResult.Add(new BookingResponse()
                {
                    Id = item.Id,
                    AccountId = item.AccountId,
                    PhoneNumber = existedAccount.PhoneNumber,
                    Total = item.Total,
                    PaymentMethod = item.PaymentMethod,
                    IsReceived = item.IsReceived,
                    ListSeatName = seatNamesString
                });
            }
            if (result.Result.Count == 0)
            {
                return Result<PaginationBaseResponse<BookingResponse>>.Fail(
                    _localizationService[LocalizationString.Category.FailedToViewList].Value.ToErrors(_localizationService));
            }
            return Result<PaginationBaseResponse<BookingResponse>>.Succeed(new PaginationBaseResponse<BookingResponse>()
            {
                CurrentPage = result.CurrentPage,
                OrderBy = result.OrderBy,
                OrderByDesc = result.OrderByDesc,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                Result = listResult
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<PaginationBaseResponse<BookingResponse>>> ViewListBookingsByAdminAsync(ViewListBookingByAdminRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var keyword = request.Keyword?.ToLower() ?? string.Empty;
            var filterQuery = await _bookingRepository.GetListBookingByAdminAsync(request, cancellationToken);
            var source = filterQuery.Where(
                u => keyword.Length <= 0
                     || u.CouponId.Contains(keyword)
            ).AsSplitQuery();
            var listResult = new List<BookingResponse>();
            var result = await _paginationService.PaginateAsync(source, request.Page, request.OrderBy, request.OrderByDesc, request.Size, cancellationToken);
            foreach (var item in result.Result)
            {
                var existedAccount = await _accountRepository.ViewMyAccountAsync(item.AccountId, cancellationToken);
                var listSeatName = new List<BookingDetailResponse>();
                var listSeatId = await _bookingRepository.GetListBookingDetailAsync(item.Id, cancellationToken);
                foreach (var item1 in listSeatId)
                {
                    var existedName = await _seatRepository.GetSeatByIdAsync(item1.SeatId, cancellationToken);
                    listSeatName.Add(new BookingDetailResponse()
                    {
                        SeatName = existedName.Name
                    });
                }
                string seatNamesString = string.Join(", ", listSeatName.Select(x => x.SeatName));
                listResult.Add(new BookingResponse()
                {
                    Id = item.Id,
                    AccountId = item.AccountId,
                    PhoneNumber = existedAccount.PhoneNumber,
                    Total = item.Total,
                    PaymentMethod = item.PaymentMethod,
                    IsReceived = item.IsReceived,
                    ListSeatName = seatNamesString
                });
            }
            if (result.Result.Count == 0)
            {
                return Result<PaginationBaseResponse<BookingResponse>>.Fail(
                    _localizationService[LocalizationString.Category.FailedToViewList].Value.ToErrors(_localizationService));
            }
            return Result<PaginationBaseResponse<BookingResponse>>.Succeed(new PaginationBaseResponse<BookingResponse>()
            {
                CurrentPage = result.CurrentPage,
                OrderBy = result.OrderBy,
                OrderByDesc = result.OrderByDesc,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                Result = listResult
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<BookingResult>> UpdateReceivedBookingAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find role
            var existedBooking = await _bookingRepository.GetBookingAsync(id, cancellationToken);
            if (existedBooking == null)
                return Result<BookingResult>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            existedBooking.IsReceived = 1;
            existedBooking.LastModified = DateTime.Now;
            existedBooking.LastModifiedById = _currentAccountService.Id;

            var resultUpdateRole = await _mediator.Send(new UpdateReceivedBookingCommand(existedBooking), cancellationToken);
            return resultUpdateRole <= 0 ? Result<BookingResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService)) : Result<BookingResult>.Succeed(_mapper.Map<BookingResult>(existedBooking));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
