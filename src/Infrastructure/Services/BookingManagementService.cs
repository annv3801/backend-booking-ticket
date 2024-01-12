using System.Drawing;
using Application.Commands.Booking;
using Application.Common.Interfaces;
using Application.DataTransferObjects.Booking.Requests;
using Application.DataTransferObjects.Booking.Responses;
using Application.Interface;
using Application.Queries.Booking;
using Application.Repositories.Booking;
using Application.Repositories.Food;
using Application.Repositories.Seat;
using Application.Services.Account;
using Application.Services.Booking;
using AutoMapper;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Nobi.Core.Responses;
using QRCoder;

// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract


namespace Infrastructure.Services;

public class BookingManagementService : IBookingManagementService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ILoggerService _loggerService;
    private readonly IBookingRepository _bookingRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly ISnowflakeIdService _snowflakeIdService;
    private readonly IBookingDetailRepository _bookingDetailRepository;
    private readonly ISeatRepository _seatRepository;
    private readonly IFoodRepository _foodRepository;
    private readonly IVnPayService _vnPayService;
    private readonly IEmailService _emaiService;
    private readonly IAccountManagementService _accountManagementService;

    public BookingManagementService(IMapper mapper, IMediator mediator, ILoggerService loggerService, IBookingRepository bookingRepository,
        IDateTimeService dateTimeService, ICurrentAccountService currentAccountService, ISnowflakeIdService snowflakeIdService, IBookingDetailRepository bookingDetailRepository, ISeatRepository seatRepository, IFoodRepository foodRepository, IVnPayService vnPayService, IEmailService emaiService, IAccountManagementService accountManagementService)
    {
        _mapper = mapper;
        _mediator = mediator;
        _loggerService = loggerService;
        _bookingRepository = bookingRepository;
        _dateTimeService = dateTimeService;
        _currentAccountService = currentAccountService;
        _snowflakeIdService = snowflakeIdService;
        _bookingDetailRepository = bookingDetailRepository;
        _seatRepository = seatRepository;
        _foodRepository = foodRepository;
        _vnPayService = vnPayService;
        _emaiService = emaiService;
        _accountManagementService = accountManagementService;
    }

    public async Task<RequestResult<bool>> CreateBookingAsync(CreateBookingRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.SeatId.Count <= 0)
                return RequestResult<bool>.Fail("Not Found Seats");
            var account = await _accountManagementService.ViewAccountDetailByAdminAsync(_currentAccountService.Id, cancellationToken);
            if(!account.Success) 
                return RequestResult<bool>.Fail("Not found account"); 
            
            // Create Booking 
            var bookingEntity = _mapper.Map<BookingEntity>(request);
            bookingEntity.Id = await _snowflakeIdService.GenerateId(cancellationToken);
            bookingEntity.Discount = 0;
            bookingEntity.Total = 0;
            bookingEntity.TotalBeforeDiscount = 0;
            bookingEntity.CouponId = request.CouponId;
            bookingEntity.PaymentMethod = request.PaymentMethod;
            bookingEntity.AccountId = _currentAccountService.Id;
            bookingEntity.CreatedBy = _currentAccountService.Id;
            bookingEntity.CreatedTime = _dateTimeService.NowUtc;
            bookingEntity.ModifiedBy = _currentAccountService.Id;
            bookingEntity.ModifiedTime = _dateTimeService.NowUtc;
            bookingEntity.Status = "PENDING";   
            
            await _bookingRepository.AddAsync(bookingEntity, cancellationToken);
            
                var seatResponse = await _seatRepository.GetSeatEntityByIdAsync(request.SeatId.First(), cancellationToken);
                
                var totalFood = (double)0;
                if (request.Foods.Count > 0)
                {
                    foreach (var item in request.Foods)
                    {
                        var foodResponse = await _foodRepository.GetFoodByIdAsync(item.FoodId, cancellationToken);
                        if (foodResponse != null)
                            totalFood += foodResponse.Price * item.Quantity;
                    }
                }
                
                bookingEntity.Total = seatResponse.Ticket.Price * request.SeatId.Count + totalFood;
                bookingEntity.TotalBeforeDiscount = seatResponse.Ticket.Price * request.SeatId.Count + totalFood;
                
                // if (request.Total != bookingEntity.Total)
                //     return RequestResult<bool>.Fail("Total not exist");
                
                foreach (var item in request.SeatId)
                {
                    await _bookingDetailRepository.AddAsync(new BookingDetailEntity()
                    {
                        Id = await _snowflakeIdService.GenerateId(cancellationToken),
                        BookingId = bookingEntity.Id,
                        SeatId = item,
                        Foods = request.Foods.Select(x => new FoodRequest()
                        {
                            Quantity = x.Quantity,
                            FoodId = x.FoodId
                        }).ToList(), 
                        CreatedBy = _currentAccountService.Id,
                        CreatedTime = _dateTimeService.NowUtc,
                        ModifiedBy = _currentAccountService.Id,
                        ModifiedTime = _dateTimeService.NowUtc
                    }, cancellationToken);
                    var seat = await _seatRepository.Entity.Where(x => x.Id == item && ( x.Status != "BOOKED" || x.Status != "DELETED")).FirstOrDefaultAsync(cancellationToken);
                    seat.Status = "BOOKED";
                    await _seatRepository.UpdateAsync(seat, cancellationToken);
                    await _seatRepository.SaveChangesAsync(cancellationToken);
                }
                await _bookingRepository.UpdateAsync(bookingEntity, cancellationToken);
                var resutl = await _bookingRepository.SaveChangesAsync(cancellationToken);
                await _bookingDetailRepository.SaveChangesAsync(cancellationToken);
                _emaiService.SendEmail(account.Data.Email, "Cảm ơn bạn đã đặt vé thành công tại Cinemax", bookingEntity.Id.ToString(), true);
                if (resutl > 0)
                {
                    return RequestResult<bool>.Succeed("Save data success");
                }
                return RequestResult<bool>.Fail("Save data fail");

        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateBookingAsync));
            throw;
        }
    }

    public async Task<RequestResult<bool>> CancelBookingAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var bookingEntity = await _bookingRepository.Entity.Where(x => x.Id == id && _currentAccountService.Id == x.CreatedBy).FirstOrDefaultAsync(cancellationToken);
            if (bookingEntity is null)
            {
                return RequestResult<bool>.Fail("Booking is not found");
            }
            if (bookingEntity.CreatedTime.HasValue && DateTimeOffset.Now.Subtract(bookingEntity.CreatedTime.Value) > TimeSpan.FromMinutes(30))
            {
                return RequestResult<bool>.Fail("Booking cannot be canceled as it was created more than 30 minutes ago");
            }
            bookingEntity.Status = "CANCELED";
            bookingEntity.ModifiedTime = _dateTimeService.NowUtc;
            bookingEntity.ModifiedBy = _currentAccountService.Id;
            var booking = await _mediator.Send(new CancelBookingCommand()
            {
                Entity = bookingEntity
            }, cancellationToken);
            if (booking == null)
                return RequestResult<bool>.Fail("Booking is not found");
            if (bookingEntity.PaymentMethod == 1)
                return RequestResult<bool>.Succeed("You need to go to the counter to pick up the slip to get your money back.");
            return RequestResult<bool>.Succeed("Cancel booking success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetBookingAsync));
            throw;
        }
    }

    public async Task<RequestResult<bool>> ChangeStatusBookingAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var bookingEntity = await _bookingRepository.Entity.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
            if (bookingEntity is null)
            {
                return RequestResult<bool>.Fail("Booking is not found");
            }
            
            bookingEntity.IsReceived = 1;
            bookingEntity.Status = "RECEIVED";
            var booking = await _mediator.Send(new ChangeStatusBookingCommand()
            {
                Entity = bookingEntity
            }, cancellationToken);
            if (booking == null)
                return RequestResult<bool>.Fail("Booking is not found");
            return RequestResult<bool>.Succeed("Change status booking success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetBookingAsync));
            throw;
        }
    }

    public async Task<RequestResult<BookingResponse>> GetBookingAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var booking = await _mediator.Send(new GetBookingByIdQuery
            {
                Id = id,
            }, cancellationToken);
            if (booking == null)
                return RequestResult<BookingResponse>.Fail("Booking is not found");
            return RequestResult<BookingResponse>.Succeed(null, _mapper.Map<BookingResponse>(booking));
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetBookingAsync));
            throw;
        }
    }

    public async Task<RequestResult<OffsetPaginationResponse<BookingResponse>>> GetListBookingsAsync(ViewListBookingsRequest request, CancellationToken cancellationToken)
    {
        var bookings = await _mediator.Send(new GetListBookingsQuery
        {
            Request = request,
            AccountId = request.AccountId
        }, cancellationToken);

        return RequestResult<OffsetPaginationResponse<BookingResponse>>.Succeed(null, bookings);
    }
}