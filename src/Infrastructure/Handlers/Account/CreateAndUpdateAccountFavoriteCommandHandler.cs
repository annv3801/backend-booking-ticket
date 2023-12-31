using Application.Commands.Account;
using Application.Common.Interfaces;
using Application.Handlers.Account;
using Application.Repositories.AccountFavorite;
using Domain.Common.Interface;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure.Handlers.Account;

public class CreateAndUpdateAccountFavoriteCommandHandler : ICreateAndUpdateAccountFavoriteHandler
{
    private readonly IAccountFavoriteRepository _accountFavoriteRepository;
    private readonly ILoggerService _loggerService;
    private readonly ICurrentAccountService _currentAccountService;

    public CreateAndUpdateAccountFavoriteCommandHandler(IAccountFavoriteRepository accountFavoriteRepository, ILoggerService loggerService, ICurrentAccountService currentAccountService)
    {
        _accountFavoriteRepository = accountFavoriteRepository;
        _loggerService = loggerService;
        _currentAccountService = currentAccountService;
    }

    public async Task<int> Handle(CreateAndUpdateAccountFavoriteCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var getAccountFavorite = await _accountFavoriteRepository.IsHaveFavorite(command.AccountId, command.FilmId, command.TheaterId, cancellationToken);
            if(getAccountFavorite != null)
            {
                await _accountFavoriteRepository.RemoveAsync(getAccountFavorite, cancellationToken);
                return await _accountFavoriteRepository.SaveChangesAsync(cancellationToken);
            }
            var accountFavorite = new AccountFavoritesEntity
            {
                AccountId = command.AccountId,
                FilmId = command.FilmId,
                TheaterId = command.TheaterId
            };
            await _accountFavoriteRepository.AddAsync(accountFavorite, cancellationToken);
            return await _accountFavoriteRepository.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(Handle));
            throw;
        }
    }
}