using Application.Commands.Theater;
using Application.Common.Interfaces;
using Application.Handlers.Theater;
using Application.Interface;
using Application.Repositories.Theater;
using Domain.Common.Interface;
using Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Handlers.Commands.Theater;

public class TheaterCommandHandler : ICreateTheaterCommandHandler, IUpdateTheaterCommandHandler, IDeleteTheaterCommandHandler
{
    private readonly ITheaterRepository _theaterRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILoggerService _loggerService;
    private readonly ICurrentAccountService _currentAccountService;

    public TheaterCommandHandler(ITheaterRepository theaterRepository, IDateTimeService dateTimeService, ILoggerService loggerService, ICurrentAccountService currentAccountService)
    {
        _theaterRepository = theaterRepository;
        _dateTimeService = dateTimeService;
        _loggerService = loggerService;
        _currentAccountService = currentAccountService;
    }

    public async Task<int> Handle(CreateTheaterCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _theaterRepository.AddAsync(command.Entity, cancellationToken);
            return await _theaterRepository.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(Handle));
            throw;
        }
    }

    public async Task<int> Handle(UpdateTheaterCommand command, CancellationToken cancellationToken)
    {
        try
        {
            return await _theaterRepository.Entity.Where(x => x.Id == command.Request.Id && x.Status != EntityStatus.Deleted)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(l => l.Name, command.Request.Name)
                    .SetProperty(l => l.Location, command.Request.Location)
                    .SetProperty(l => l.Longitude, command.Request.Longitude)
                    .SetProperty(l => l.Latitude, command.Request.Latitude)
                    .SetProperty(l => l.PhoneNumber, command.Request.PhoneNumber)
                    .SetProperty(l => l.ModifiedBy, _currentAccountService.Id)
                    .SetProperty(l => l.ModifiedTime, _dateTimeService.NowUtc), cancellationToken);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(Handle));
            throw;
        }
    }

    public async Task<int> Handle(DeleteTheaterCommand command, CancellationToken cancellationToken)
    {
        try
        {
            return await _theaterRepository.Entity.Where(x => x.Id == command.Id && x.Status != EntityStatus.Deleted)
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