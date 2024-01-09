using Application.DTO.Role.Responses;
using Application.Interface;
using Application.Queries.Role;
using Application.Repositories.Role;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common.Repository;
using Domain.Enums;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Role
{
    public class RoleRepository : Repository<Domain.Entities.Identity.Role, ApplicationDbContext>, IRoleRepository
    {
        private readonly DbSet<Domain.Entities.Identity.Role> _rolesEntity;
        private readonly IMapper _mapper;

        public RoleRepository(ApplicationDbContext applicationDbContext, ISnowflakeIdService snowflakeIdService, IMapper mapper) : base(applicationDbContext, snowflakeIdService)
        {
            _mapper = mapper;
            _rolesEntity = applicationDbContext.Set<Domain.Entities.Identity.Role>();
        }

        public async Task<Domain.Entities.Identity.Role?> GetRoleAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _rolesEntity.Include(r => r.RolePermissions).Where(r => r.NormalizedName == name.ToUpper()).AsSplitQuery().FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<Domain.Entities.Identity.Role?> GetRoleAsync(long roleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _rolesEntity.Include(r => r.RolePermissions).AsSplitQuery().FirstOrDefaultAsync(r => r.Id == roleId, cancellationToken);
        }

        public async Task<List<Domain.Entities.Identity.Role>?> GetRolesAsync(List<long> roleIds, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _rolesEntity.Where(r => roleIds.Contains(r.Id) && r.Status == RoleStatus.Active)
                .AsSplitQuery().ToListAsync(cancellationToken);
        }

        public async Task<Domain.Entities.Identity.Role?> GetRoleToViewDetail(long roleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _rolesEntity
                .Include(r => r.CreatedBy)
                .Include(r => r.RolePermissions)
                .ThenInclude(p => p.Permission)
                .AsSplitQuery()
                .FirstOrDefaultAsync(r => r.Id == roleId, cancellationToken);
        }

        public async Task<IQueryable<ViewRoleResponse>> SearchRoleByName(ViewListRolesQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.CompletedTask;
            return _rolesEntity
                .Include(r => r.CreatedBy)
                .Include(r => r.RolePermissions)
                .ThenInclude(p => p.Permission)
                .ProjectTo<ViewRoleResponse>(_mapper.ConfigurationProvider)
                .AsSplitQuery();
        }
    }
}