using Application.DataTransferObjects.Permission.Responses;
using Application.Interface;
using Application.Queries.Permission;
using Application.Repositories.Permission;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common.Repository;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Permission
{
    
    public class PermissionRepository : Repository<Domain.Entities.Identity.Permission, ApplicationDbContext>, IPermissionRepository
    {
        private readonly DbSet<Domain.Entities.Identity.Permission> _permissionsEntity;
        private readonly IMapper _mapper;

        public PermissionRepository(ApplicationDbContext applicationDbContext, ISnowflakeIdService snowflakeIdService, IMapper mapper) : base(applicationDbContext, snowflakeIdService)
        {
            _mapper = mapper;
            _permissionsEntity = applicationDbContext.Set<Domain.Entities.Identity.Permission>();
        }

        ///<inheritdoc/>
        public async Task<Domain.Entities.Identity.Permission?> FindPermissionById(long permissionId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _permissionsEntity.FirstOrDefaultAsync(p => p.Id == permissionId, cancellationToken);
        }

        public async Task<Domain.Entities.Identity.Permission?> FindPermissionByIdWithAuditableEntity(long permissionId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _permissionsEntity
                .Include(p => p.CreatedBy)
                .FirstOrDefaultAsync(p => p.Id == permissionId, cancellationToken);
        }

        public async Task<IQueryable<ViewPermissionResponse>> SearchPermissionByName(ViewListPermissionsQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.CompletedTask;
            return _permissionsEntity
                .Include(p => p.CreatedBy)
                .ProjectTo<ViewPermissionResponse>(_mapper.ConfigurationProvider)
                .AsSplitQuery();
        }

      
    }
}