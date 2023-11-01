using Application.Commands.Film;
using Application.Common.Interfaces;
using Application.Handlers.Film;
using Application.Interface;
using Application.Repositories.Film;
using Domain.Common.Interface;
using Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Handlers.Commands.Film;

public class FilmCommandHandler : ICreateFilmCommandHandler, IUpdateFilmCommandHandler, IDeleteFilmCommandHandler, ICreateFeedbackFilmCommandHandler
{
    private readonly IFilmRepository _filmRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILoggerService _loggerService;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly IFeedbackFilmRepository _feedbackFilmRepository;

    public FilmCommandHandler(IFilmRepository filmRepository, IDateTimeService dateTimeService, ILoggerService loggerService, ICurrentAccountService currentAccountService, IFeedbackFilmRepository feedbackFilmRepository)
    {
        _filmRepository = filmRepository;
        _dateTimeService = dateTimeService;
        _loggerService = loggerService;
        _currentAccountService = currentAccountService;
        _feedbackFilmRepository = feedbackFilmRepository;
    }

    public async Task<int> Handle(CreateFilmCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _filmRepository.AddAsync(command.Entity, cancellationToken);
            return await _filmRepository.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(Handle));
            throw;
        }
    }

    public async Task<int> Handle(UpdateFilmCommand command, CancellationToken cancellationToken)
    {
        try
        {
            return await _filmRepository.Entity.Where(x => x.Id == command.Request.Id && x.Status != EntityStatus.Deleted)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(l => l.Name, command.Request.Name)
                    .SetProperty(l => l.Slug, command.Request.Slug)
                    .SetProperty(l => l.GroupEntityId, command.Request.GroupEntityId)
                    .SetProperty(l => l.CategoryIds, command.Request.CategoryIds)
                    .SetProperty(l => l.Description, command.Request.Description)
                    .SetProperty(l => l.Director, command.Request.Director)
                    .SetProperty(l => l.Actor, command.Request.Actor)
                    .SetProperty(l => l.Genre, command.Request.Genre)
                    .SetProperty(l => l.Premiere, command.Request.Premiere)
                    .SetProperty(l => l.Duration, command.Request.Duration)
                    .SetProperty(l => l.Language, command.Request.Language)
                    .SetProperty(l => l.Rated, command.Request.Rated)
                    .SetProperty(l => l.Trailer, command.Request.Trailer)
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

    public async Task<int> Handle(DeleteFilmCommand command, CancellationToken cancellationToken)
    {
        try
        {
            return await _filmRepository.Entity.Where(x => x.Id == command.Id && x.Status != EntityStatus.Deleted)
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
    
    public async Task<int> Handle(CreateFeedbackFilmCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _feedbackFilmRepository.AddAsync(command.Entity, cancellationToken);
            return await _feedbackFilmRepository.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(Handle));
            throw;
        }
    }
}