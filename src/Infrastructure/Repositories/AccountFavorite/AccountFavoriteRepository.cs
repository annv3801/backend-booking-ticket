using Application.Interface;
using Application.Repositories.AccountFavorite;
using AutoMapper;
using Domain.Common.Repository;
using Domain.Entities;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.AccountFavorite;

public class AccountFavoriteRepository : Repository<AccountFavoritesEntity, ApplicationDbContext>, IAccountFavoriteRepository
{
    private readonly DbSet<AccountFavoritesEntity> _accountFavoriteEntities;
    private readonly IMapper _mapper;

    public AccountFavoriteRepository(ApplicationDbContext applicationDbContext, IMapper mapper, ISnowflakeIdService snowflakeIdService) : base(applicationDbContext, snowflakeIdService)
    {
        _mapper = mapper;
        _accountFavoriteEntities = applicationDbContext.Set<AccountFavoritesEntity>();
    }

    public async Task<List<AccountFavoritesEntity>> GetListFavoriteByAccountId(long id, CancellationToken cancellationToken)
    {
        return await _accountFavoriteEntities.AsNoTracking().Where(x => x.AccountId == id).ToListAsync(cancellationToken);
    }

    public async Task<AccountFavoritesEntity> IsHaveFavorite(long accountId, long? filmId, long? theaterId, CancellationToken cancellationToken)
    {
        if(filmId is null || filmId == 0)
            return await _accountFavoriteEntities.FirstOrDefaultAsync(x => x.AccountId == accountId && x.TheaterId == theaterId, cancellationToken);
        return await  _accountFavoriteEntities.FirstOrDefaultAsync(x => x.AccountId == accountId && x.FilmId == filmId, cancellationToken);
    }
}