using Application.Commands.Seat;
using Application.Common.Interfaces;
using Application.Handlers.Seat;
using Application.Interface;
using Application.Repositories.Seat;
using Domain.Common.Interface;
using Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Handlers.Commands.Seat;

public class SeatCommandHandler : ICreateSeatCommandHandler, IUpdateSeatCommandHandler, IDeleteSeatCommandHandler
{
    private readonly ISeatRepository _seatRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILoggerService _loggerService;
    private readonly ICurrentAccountService _currentAccountService;

    public SeatCommandHandler(ISeatRepository seatRepository, IDateTimeService dateTimeService, ILoggerService loggerService, ICurrentAccountService currentAccountService)
    {
        _seatRepository = seatRepository;
        _dateTimeService = dateTimeService;
        _loggerService = loggerService;
        _currentAccountService = currentAccountService;
    }

    public async Task<int> Handle(CreateListSeatCommand command, CancellationToken cancellationToken)
    {
        try
        {
            foreach (var item in command.Entity)
            {
                await _seatRepository.AddAsync(item, cancellationToken);
            }
            return await _seatRepository.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(Handle));
            throw;
        }
    }

    public async Task<int> Handle(UpdateSeatCommand command, CancellationToken cancellationToken)
    {
        try
        {
            return await _seatRepository.Entity.Where(x => x.Id == command.Request.Id && x.Status != EntityStatus.Deleted)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(l => l.SchedulerId, command.Request.SchedulerId)
                    .SetProperty(l => l.RoomSeatId, command.Request.RoomSeatId)
                    .SetProperty(l => l.Type, command.Request.Type)
                    .SetProperty(l => l.ModifiedBy, _currentAccountService.Id)
                    .SetProperty(l => l.ModifiedTime, _dateTimeService.NowUtc), cancellationToken);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(Handle));
            throw;
        }
    }

    public async Task<int> Handle(DeleteSeatCommand command, CancellationToken cancellationToken)
    {
        try
        {
            return await _seatRepository.Entity.Where(x => x.Id == command.Id && x.Status != EntityStatus.Deleted)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(l => l.DeletedTime, _dateTimeService.NowUtc)
                    .SetProperty(l => l.DeletedBy, _currentAccountService.Id)
                    .SetProperty(l => l.Deleted, true)
                    .SetProperty(l => l.Status, EntityStatus.Deleted), cancellationToken);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(Handle));
            throw;
        }
    }
}