using Application.Commands.Booking;
using Application.Common.Interfaces;
using Application.DataTransferObjects.Booking.Requests;
using Application.DataTransferObjects.Booking.Responses;
using Application.Interface;
using Application.Queries.Booking;
using Application.Repositories.Booking;
using Application.Repositories.Seat;
using Application.Services.Booking;
using AutoMapper;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nobi.Core.Responses;

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

    public BookingManagementService(IMapper mapper, IMediator mediator, ILoggerService loggerService, IBookingRepository bookingRepository,
        IDateTimeService dateTimeService, ICurrentAccountService currentAccountService, ISnowflakeIdService snowflakeIdService, IBookingDetailRepository bookingDetailRepository, ISeatRepository seatRepository)
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
    }

    public async Task<RequestResult<bool>> CreateBookingAsync(CreateBookingRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // Create Booking 
            var bookingEntity = _mapper.Map<BookingEntity>(request);
            bookingEntity.Id = await _snowflakeIdService.GenerateId(cancellationToken);
            bookingEntity.Discount = request.Discount;
            bookingEntity.Total = request.Total;
            bookingEntity.TotalBeforeDiscount = request.TotalBeforeDiscont;
            bookingEntity.CouponId = request.CouponId;
            bookingEntity.PaymentMethod = request.PaymentMethod;
            bookingEntity.AccountId = _currentAccountService.Id;
            bookingEntity.CreatedBy = _currentAccountService.Id;
            bookingEntity.CreatedTime = _dateTimeService.NowUtc;
            bookingEntity.ModifiedBy = _currentAccountService.Id;
            bookingEntity.ModifiedTime = _dateTimeService.NowUtc;
            
            var resultCreateBooking = await _mediator.Send(new CreateBookingCommand {Entity = bookingEntity}, cancellationToken);
            if (resultCreateBooking > 0)
            {
                foreach (var item in request.SeatId)
                {
                    await _bookingDetailRepository.AddAsync(new BookingDetailEntity()
                    {
                        Id = await _snowflakeIdService.GenerateId(cancellationToken),
                        BookingId = bookingEntity.Id,
                        SeatId = item,
                        CreatedBy = _currentAccountService.Id,
                        CreatedTime = _dateTimeService.NowUtc,
                        ModifiedBy = _currentAccountService.Id,
                        ModifiedTime = _dateTimeService.NowUtc
                    }, cancellationToken);
                    var seat = await _seatRepository.Entity.Where(x => x.Id == item && x.Status != "BOOKED" || x.Status != "DELETED").FirstOrDefaultAsync(cancellationToken);
                    seat.Status = "BOOKED";
                    await _seatRepository.UpdateAsync(seat, cancellationToken);
                }
                await _bookingDetailRepository.SaveChangesAsync(cancellationToken);
                return RequestResult<bool>.Succeed("Save data success");
            }
            return RequestResult<bool>.Fail("Save data failed");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateBookingAsync));
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