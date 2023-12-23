using Application.DataTransferObjects.Slide.Responses;
using Application.Interface;
using Application.Repositories.Slide;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Constants;
using Domain.Entities;
using Domain.Extensions;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Slide;

public class SlideRepository : Repository<SlideEntity, ApplicationDbContext>, ISlideRepository
{
    private readonly DbSet<SlideEntity> _slideEntities;
    private readonly IMapper _mapper;

    public SlideRepository(ApplicationDbContext applicationDbContext, IMapper mapper, ISnowflakeIdService snowflakeIdService) : base(applicationDbContext, snowflakeIdService)
    {
        _mapper = mapper;
        _slideEntities = applicationDbContext.Set<SlideEntity>();
    }

    public async Task<OffsetPaginationResponse<SlideResponse>> GetListSlidesAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        var query = _slideEntities.Where(x => !x.Deleted).OrderBy(x => x.Name.ToLower()).Select(x => new SlideResponse()
            {
                Name = x.Name,
                Status = x.Status,
                Id = x.Id,
                ObjectId = x.ObjectId,
                Image = x.Image
            });
        
        var response = await query.PaginateAsync<SlideEntity,SlideResponse>(request, cancellationToken);
        return new OffsetPaginationResponse<SlideResponse>()
        {
            Data = response.Data,
            PageSize = response.PageSize,
            Total = response.Total,
            CurrentPage = response.CurrentPage
        };
    }

    public async Task<SlideResponse?> GetSlideByIdAsync(long id, CancellationToken cancellationToken)
    {
        
        return await _slideEntities.AsNoTracking().ProjectTo<SlideResponse>(_mapper.ConfigurationProvider).Where(x => x.Id == id && x.Status != EntityStatus.Deleted).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<SlideEntity?> GetSlideEntityByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _slideEntities.AsNoTracking().Where(x => x.Id == id && x.Status != EntityStatus.Deleted).FirstOrDefaultAsync(cancellationToken);
    }
}