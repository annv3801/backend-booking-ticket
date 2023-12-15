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
    private readonly ILoggerService _loggerService;

    public BookingCommandHandler(IBookingRepository bookingRepository, ILoggerService loggerService)
    {
        _bookingRepository = bookingRepository;
        _loggerService = loggerService;
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