using Application.DataTransferObjects.Film.Responses;
using Application.Interface;
using Application.Repositories.Film;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common.Repository;
using Domain.Constants;
using Domain.Entities;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Film;

public class FeedbackFilmRepository : Repository<FilmFeedbackEntity, ApplicationDbContext>, IFeedbackFilmRepository
{
    private readonly DbSet<FilmFeedbackEntity> _feedbackEntities;
    private readonly IMapper _mapper;

    public FeedbackFilmRepository(ApplicationDbContext applicationDbContext, IMapper mapper, ISnowflakeIdService snowflakeIdService) : base(applicationDbContext, snowflakeIdService)
    {
        _mapper = mapper;
        _feedbackEntities = applicationDbContext.Set<FilmFeedbackEntity>();
    }
    
    public async Task<bool> GetFeedbackByAccountIdAndFilmIdAsync(long accountId, long filmId, CancellationToken cancellationToken)
    {
        return await _feedbackEntities.AsNoTracking().AnyAsync(x => x.AccountId == accountId && x.FilmId == filmId, cancellationToken: cancellationToken);
    }

}