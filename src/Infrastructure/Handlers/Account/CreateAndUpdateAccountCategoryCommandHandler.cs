using Application.Commands.Account;
using Application.Common.Interfaces;
using Application.Handlers.Account;
using Application.Repositories.AccountCategory;
using Application.Repositories.Category;
using Domain.Common.Interface;
using Domain.Entities;

namespace Infrastructure.Handlers.Account;

public class CreateAndUpdateAccountCategoryCommandHandler : ICreateAndUpdateAccountCategoryHandler
{
    private readonly IAccountCategoryRepository _accountCategoryRepository;
    private readonly ILoggerService _loggerService;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly ICategoryRepository _categoryRepository;

    public CreateAndUpdateAccountCategoryCommandHandler(IAccountCategoryRepository accountCategoryRepository, ILoggerService loggerService, ICurrentAccountService currentAccountService, ICategoryRepository categoryRepository)
    {
        _accountCategoryRepository = accountCategoryRepository;
        _loggerService = loggerService;
        _currentAccountService = currentAccountService;
        _categoryRepository = categoryRepository;
    }

    public async Task<int> Handle(CreateAndUpdateAccountCategoryCommand command, CancellationToken cancellationToken)
    {
        try
        {
            // Find the document with id if any
            var existingAccountInAccountCategory = await _accountCategoryRepository.GetListCategoryByAccountId(_currentAccountService.Id, cancellationToken);
            if (existingAccountInAccountCategory.Count > 0)
            {
                await _accountCategoryRepository.RemoveRangeAsync(existingAccountInAccountCategory, cancellationToken);
                // Add new
                foreach (var item in command.CategoryIds)
                {
                    var checkValid = await _categoryRepository.GetCategoryByIdAsync(item, cancellationToken);
                    if (checkValid == null) 
                        return 0;
                    await _accountCategoryRepository.AddAsync(new AccountCategoryEntity()
                    {
                        CategoryId = item,
                        AccountId = _currentAccountService.Id
                    }, cancellationToken);
                }
            }
            else
            {
                // Add new
                foreach (var item in command.CategoryIds)
                {
                    var checkValid = await _categoryRepository.GetCategoryByIdAsync(item, cancellationToken);
                    if (checkValid == null)
                        return 0;
                    await _accountCategoryRepository.AddAsync(new AccountCategoryEntity()
                    {
                        CategoryId = item,
                        AccountId = _currentAccountService.Id
                    }, cancellationToken);
                }
            }
            await _accountCategoryRepository.SaveChangesAsync(cancellationToken);

            return 1;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(Handle));
            throw;
        }
    }
}