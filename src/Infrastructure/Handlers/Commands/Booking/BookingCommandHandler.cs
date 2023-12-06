using Application.Commands.Booking;
using Application.Common.Interfaces;
using Application.Handlers.Booking;
using Application.Interface;
using Application.Repositories.Booking;
using Domain.Common.Interface;
using Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Handlers.Commands.Booking;

public class BookingCommandHandler : ICreateBookingCommandHandler
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IBookingDetailRepository _bookingDetailRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILoggerService _loggerService;
    private readonly ICurrentAccountService _currentAccountService;

    public BookingCommandHandler(IBookingRepository bookingRepository, IDateTimeService dateTimeService, ILoggerService loggerService, ICurrentAccountService currentAccountService, IBookingDetailRepository bookingDetailRepository)
    {
        _bookingRepository = bookingRepository;
        _dateTimeService = dateTimeService;
        _loggerService = loggerService;
        _currentAccountService = currentAccountService;
        _bookingDetailRepository = bookingDetailRepository;
    }

    public async Task<int> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _bookingRepository.AddAsync(command.Entity, cancellationToken);
            return await _bookingRepository.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(Handle));
            throw;
        }
    }
}