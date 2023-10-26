using Application.Interface;
using Application.Repositories.AccountCategory;
using AutoMapper;
using Domain.Common.Repository;
using Domain.Entities;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.AccountCategory;

public class AccountCategoryRepository : Repository<AccountCategoryEntity, ApplicationDbContext>, IAccountCategoryRepository
{
    private readonly DbSet<AccountCategoryEntity> _accountCategoryEntities;
    private readonly IMapper _mapper;

    public AccountCategoryRepository(ApplicationDbContext applicationDbContext, IMapper mapper, ISnowflakeIdService snowflakeIdService) : base(applicationDbContext, snowflakeIdService)
    {
        _mapper = mapper;
        _accountCategoryEntities = applicationDbContext.Set<AccountCategoryEntity>();
    }

    public async Task<List<AccountCategoryEntity>> GetListCategoryByAccountId(long id, CancellationToken cancellationToken)
    {
        return await _accountCategoryEntities.AsNoTracking().Where(x => x.AccountId == id).ToListAsync(cancellationToken);
    }
}