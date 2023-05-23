using System.Diagnostics.CodeAnalysis;
using Application.Commands.Booking;
using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DataTransferObjects.Booking.Requests;
using Application.Handlers.Booking;
using Application.Repositories.Booking;
using Application.Repositories.Seat;
using Application.Services.Booking;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Handlers.Booking;

[ExcludeFromCodeCoverage]
public class BookingHandler : IBookingHandlers
{
    private readonly IBookingRepository  _bookingRepository;
    private readonly IMapper _mapper;
    private readonly IBookingManagementService _bookingManagementService;
    private readonly ISeatRepository _seatRepository;
    private readonly ICurrentAccountService _currentAccountService;

    public BookingHandler(IMapper mapper, IBookingManagementService bookingManagementService, IBookingRepository bookingRepository, ISeatRepository seatRepository, ICurrentAccountService currentAccountService)
    {
        _mapper = mapper;
        _bookingManagementService = bookingManagementService;
        _bookingRepository = bookingRepository;
        _seatRepository = seatRepository;
        _currentAccountService = currentAccountService;
    }

    public async Task<Result<BookingResult>> Handle(BookingCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var booking = _mapper.Map<Domain.Entities.Booking>(command);
            booking.Id = Guid.NewGuid();
            booking.AccountId = _currentAccountService.Id;
            booking.Status = 1;
            var bookingDetails = command.SeatId.Select(x => new BookingDetail
            {
                BookingId = booking.Id,
                ScheduleId = command.ScheduleId,
                SeatId = x
            });

            await _bookingRepository.AddAsync(booking, cancellationToken);
            await _bookingRepository.AddRangeAsync(bookingDetails, cancellationToken);
            
            foreach (var seatId in command.SeatId)
            {
                var seat = await _seatRepository.GetSeatByIdAsync(seatId, cancellationToken);
                seat.Status = 0;
                _seatRepository.Update(seat);
            }

            var bookingRequestMap = _mapper.Map<BookingRequest>(booking);
            bookingRequestMap.ScheduleId = command.ScheduleId;
            bookingRequestMap.SeatId = command.SeatId;
            var result = await _bookingManagementService.BookingAsync(bookingRequestMap, cancellationToken);
            if (result.Succeeded)
                return Result<BookingResult>.Succeed(data: _mapper.Map<BookingResult>(booking));
            return Result<BookingResult>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<BookingResult>.Fail(Constants.CommitFailed);
        }
    }
}
