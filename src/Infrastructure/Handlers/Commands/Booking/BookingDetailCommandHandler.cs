using Application.Commands.Booking;
using Application.Common.Interfaces;
using Application.Handlers.Booking;
using Application.Interface;
using Application.Repositories.Booking;
using Domain.Common.Interface;

namespace Infrastructure.Handlers.Commands.Booking;

public class BookingDetailCommandHandler : ICreateBookingDetailCommandHandler
{
    private readonly IBookingDetailRepository _bookingDetailRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILoggerService _loggerService;
    private readonly ICurrentAccountService _currentAccountService;

    public BookingDetailCommandHandler(IBookingDetailRepository bookingDetailRepository, IDateTimeService dateTimeService, ILoggerService loggerService, ICurrentAccountService currentAccountService)
    {
        _bookingDetailRepository = bookingDetailRepository;
        _dateTimeService = dateTimeService;
        _loggerService = loggerService;
        _currentAccountService = currentAccountService;
    }

    public async Task<int> Handle(CreateBookingDetailCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _bookingDetailRepository.AddAsync(command.Entity, cancellationToken);
            return await _bookingDetailRepository.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(Handle));
            throw;
        }
    }
}