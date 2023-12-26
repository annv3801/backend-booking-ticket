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
            await _filmRepository.UpdateAsync(command.Request, cancellationToken);
            return await _filmRepository.SaveChangesAsync(cancellationToken);
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
            var filmEntity = await _filmRepository.GetFilmEntityByIdAsync(command.Id, cancellationToken);
            if (filmEntity is null)
                return 0;
            await _filmRepository.UpdateAsync(filmEntity, cancellationToken);
            return await _filmRepository.SaveChangesAsync(cancellationToken);
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