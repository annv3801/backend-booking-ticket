using Application.Commands.News;
using Application.Common.Interfaces;
using Application.Handlers.News;
using Application.Interface;
using Application.Repositories.News;
using Domain.Common.Interface;
using Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Handlers.Commands.News;

public class NewsCommandHandler : ICreateNewsCommandHandler, IUpdateNewsCommandHandler, IDeleteNewsCommandHandler
{
    private readonly INewsRepository _newsRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILoggerService _loggerService;
    private readonly ICurrentAccountService _currentAccountService;

    public NewsCommandHandler(INewsRepository newsRepository, IDateTimeService dateTimeService, ILoggerService loggerService, ICurrentAccountService currentAccountService)
    {
        _newsRepository = newsRepository;
        _dateTimeService = dateTimeService;
        _loggerService = loggerService;
        _currentAccountService = currentAccountService;
    }

    public async Task<int> Handle(CreateNewsCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _newsRepository.AddAsync(command.Entity, cancellationToken);
            return await _newsRepository.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(Handle));
            throw;
        }
    }

    public async Task<int> Handle(UpdateNewsCommand command, CancellationToken cancellationToken)
    {
        try
        {
            return await _newsRepository.Entity.Where(x => x.Id == command.Request.Id && x.Status != EntityStatus.Deleted)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(l => l.Title, command.Request.Title)
                    .SetProperty(l => l.GroupEntityId, command.Request.GroupEntityId)
                    .SetProperty(l => l.Image, command.Request.Image)
                    .SetProperty(l => l.Description, command.Request.Description)
                    .SetProperty(l => l.ModifiedBy, _currentAccountService.Id)
                    .SetProperty(l => l.ModifiedTime, _dateTimeService.NowUtc), cancellationToken);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(Handle));
            throw;
        }
    }

    public async Task<int> Handle(DeleteNewsCommand command, CancellationToken cancellationToken)
    {
        try
        {
            return await _newsRepository.Entity.Where(x => x.Id == command.Id && x.Status != EntityStatus.Deleted)
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