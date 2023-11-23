using Application.Commands.Scheduler;
using Application.Common.Interfaces;
using Application.Handlers.Scheduler;
using Application.Interface;
using Application.Repositories.Scheduler;
using Domain.Common.Interface;
using Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Handlers.Commands.Scheduler;

public class SchedulerCommandHandler : ICreateSchedulerCommandHandler, IUpdateSchedulerCommandHandler, IDeleteSchedulerCommandHandler
{
    private readonly ISchedulerRepository _schedulerRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILoggerService _loggerService;
    private readonly ICurrentAccountService _currentAccountService;

    public SchedulerCommandHandler(ISchedulerRepository schedulerRepository, IDateTimeService dateTimeService, ILoggerService loggerService, ICurrentAccountService currentAccountService)
    {
        _schedulerRepository = schedulerRepository;
        _dateTimeService = dateTimeService;
        _loggerService = loggerService;
        _currentAccountService = currentAccountService;
    }

    public async Task<int> Handle(CreateSchedulerCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _schedulerRepository.AddAsync(command.Entity, cancellationToken);
            return await _schedulerRepository.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(Handle));
            throw;
        }
    }

    public async Task<int> Handle(UpdateSchedulerCommand command, CancellationToken cancellationToken)
    {
        try
        {
            return await _schedulerRepository.Entity.Where(x => x.Id == command.Request.Id && x.Status != EntityStatus.Deleted)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(l => l.FilmId, command.Request.FilmId)
                    .SetProperty(l => l.RoomId, command.Request.RoomId)
                    .SetProperty(l => l.StartTime, command.Request.StartTime)
                    .SetProperty(l => l.EndTime, command.Request.EndTime)
                    .SetProperty(l => l.ModifiedBy, _currentAccountService.Id)
                    .SetProperty(l => l.ModifiedTime, _dateTimeService.NowUtc), cancellationToken);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(Handle));
            throw;
        }
    }

    public async Task<int> Handle(DeleteSchedulerCommand command, CancellationToken cancellationToken)
    {
        try
        {
            return await _schedulerRepository.Entity.Where(x => x.Id == command.Id && x.Status != EntityStatus.Deleted)
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