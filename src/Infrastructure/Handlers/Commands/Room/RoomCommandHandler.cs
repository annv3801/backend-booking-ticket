using Application.Commands.Room;
using Application.Common.Interfaces;
using Application.Handlers.Room;
using Application.Interface;
using Application.Repositories.Room;
using Domain.Common.Interface;
using Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Handlers.Commands.Room;

public class RoomCommandHandler : ICreateRoomCommandHandler, IUpdateRoomCommandHandler, IDeleteRoomCommandHandler
{
    private readonly IRoomRepository _roomRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILoggerService _loggerService;
    private readonly ICurrentAccountService _currentAccountService;

    public RoomCommandHandler(IRoomRepository roomRepository, IDateTimeService dateTimeService, ILoggerService loggerService, ICurrentAccountService currentAccountService)
    {
        _roomRepository = roomRepository;
        _dateTimeService = dateTimeService;
        _loggerService = loggerService;
        _currentAccountService = currentAccountService;
    }

    public async Task<int> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _roomRepository.AddAsync(command.Entity, cancellationToken);
            return await _roomRepository.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(Handle));
            throw;
        }
    }

    public async Task<int> Handle(UpdateRoomCommand command, CancellationToken cancellationToken)
    {
        try
        {
            return await _roomRepository.Entity.Where(x => x.Id == command.Request.Id && x.Status != EntityStatus.Deleted)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(l => l.Name, command.Request.Name)
                    .SetProperty(l => l.TheaterId, command.Request.TheaterId)
                    .SetProperty(l => l.ModifiedBy, _currentAccountService.Id)
                    .SetProperty(l => l.ModifiedTime, _dateTimeService.NowUtc), cancellationToken);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(Handle));
            throw;
        }
    }

    public async Task<int> Handle(DeleteRoomCommand command, CancellationToken cancellationToken)
    {
        try
        {
            return await _roomRepository.Entity.Where(x => x.Id == command.Id && x.Status != EntityStatus.Deleted)
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