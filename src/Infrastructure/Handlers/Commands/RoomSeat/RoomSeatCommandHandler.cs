using Application.Commands.RoomSeat;
using Application.Common.Interfaces;
using Application.Handlers.RoomSeat;
using Application.Interface;
using Application.Repositories.RoomSeat;
using Domain.Common.Interface;
using Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Handlers.Commands.RoomSeat;

public class RoomSeatCommandHandler : ICreateRoomSeatCommandHandler, IUpdateRoomSeatCommandHandler, IDeleteRoomSeatCommandHandler
{
    private readonly IRoomSeatRepository _roomSeatRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILoggerService _loggerService;
    private readonly ICurrentAccountService _currentAccountService;

    public RoomSeatCommandHandler(IRoomSeatRepository roomSeatRepository, IDateTimeService dateTimeService, ILoggerService loggerService, ICurrentAccountService currentAccountService)
    {
        _roomSeatRepository = roomSeatRepository;
        _dateTimeService = dateTimeService;
        _loggerService = loggerService;
        _currentAccountService = currentAccountService;
    }

    public async Task<int> Handle(CreateRoomSeatCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _roomSeatRepository.AddAsync(command.Entity, cancellationToken);
            return await _roomSeatRepository.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(Handle));
            throw;
        }
    }

    public async Task<int> Handle(UpdateRoomSeatCommand command, CancellationToken cancellationToken)
    {
        try
        {
            return await _roomSeatRepository.Entity.Where(x => x.Id == command.Request.Id && x.Status != EntityStatus.Deleted)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(l => l.Name, command.Request.Name)
                    .SetProperty(l => l.RoomId, command.Request.RoomId)
                    .SetProperty(l => l.ModifiedBy, _currentAccountService.Id)
                    .SetProperty(l => l.ModifiedTime, _dateTimeService.NowUtc), cancellationToken);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(Handle));
            throw;
        }
    }

    public async Task<int> Handle(DeleteRoomSeatCommand command, CancellationToken cancellationToken)
    {
        try
        {
            return await _roomSeatRepository.Entity.Where(x => x.Id == command.Id && x.Status != EntityStatus.Deleted)
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