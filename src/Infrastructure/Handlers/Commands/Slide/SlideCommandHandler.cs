using Application.Commands.Slide;
using Application.Common.Interfaces;
using Application.Handlers.Slide;
using Application.Interface;
using Application.Repositories.Slide;
using Domain.Common.Interface;
using Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Handlers.Commands.Slide;

public class SlideCommandHandler : ICreateSlideCommandHandler, IUpdateSlideCommandHandler, IDeleteSlideCommandHandler
{
    private readonly ISlideRepository _slideRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILoggerService _loggerService;
    private readonly ICurrentAccountService _currentAccountService;

    public SlideCommandHandler(ISlideRepository slideRepository, IDateTimeService dateTimeService, ILoggerService loggerService, ICurrentAccountService currentAccountService)
    {
        _slideRepository = slideRepository;
        _dateTimeService = dateTimeService;
        _loggerService = loggerService;
        _currentAccountService = currentAccountService;
    }

    public async Task<int> Handle(CreateSlideCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _slideRepository.AddAsync(command.Entity, cancellationToken);
            return await _slideRepository.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(Handle));
            throw;
        }
    }

    public async Task<int> Handle(UpdateSlideCommand command, CancellationToken cancellationToken)
    {
        try
        {
            return await _slideRepository.Entity.Where(x => x.Id == command.Request.Id && x.Status != EntityStatus.Deleted)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(l => l.Name, command.Request.Name)
                    .SetProperty(l => l.Image, command.Request.Image)
                    .SetProperty(l => l.ModifiedBy, _currentAccountService.Id)
                    .SetProperty(l => l.ModifiedTime, _dateTimeService.NowUtc), cancellationToken);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(Handle));
            throw;
        }
    }

    public async Task<int> Handle(DeleteSlideCommand command, CancellationToken cancellationToken)
    {
        try
        {
            return await _slideRepository.Entity.Where(x => x.Id == command.Id && x.Status != EntityStatus.Deleted)
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