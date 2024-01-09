using Application.Common;
using Application.Common.Interfaces;
using Application.DTO.Role.Responses;
using Application.Queries.Role;
using Application.Repositories.Role;
using Application.Services.Role;
using AutoMapper;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Domain.Enums;
using Domain.Extensions;
using Nobi.Core.Responses;

namespace Infrastructure.Services
{
    public class RoleManagementService : IRoleManagementService
    {
        private readonly ILoggerService _loggerService;
        private readonly IStringLocalizationService _localizationService;
        private readonly IJsonSerializerService _jsonSerializerService;
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="loggerService"></param>
        /// <param name="actionLogService"></param>
        /// <param name="localizationService"></param>
        /// <param name="jsonSerializerService"></param>
        /// <param name="mapper"></param>
        /// <param name="paginationService"></param>
        public RoleManagementService(ILoggerService loggerService, IStringLocalizationService localizationService, IJsonSerializerService jsonSerializerService, IMapper mapper, IRoleRepository roleRepository)
        {
            _loggerService = loggerService;
            _localizationService = localizationService;
            _jsonSerializerService = jsonSerializerService;
            _mapper = mapper;
            _roleRepository = roleRepository;
        }


        /// <inheritdoc />
        public async Task<RequestResult<RoleResult>> CreateRoleAsync(Domain.Entities.Identity.Role role, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                // Find role
                var existedRole = await _roleRepository.GetRoleAsync(role.Name, cancellationToken);
                if (existedRole != null)
                    return RequestResult<RoleResult>.Fail("Not Found Role");
                await _roleRepository.AddAsync(role, cancellationToken);
                var result = await _roleRepository.SaveChangesAsync(cancellationToken);
                if (result > 0)
                {
                    return RequestResult<RoleResult>.Succeed();
                }
                
                return RequestResult<RoleResult>.Fail("Fail");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<RequestResult<RoleResult>> UpdateRoleAsync(Domain.Entities.Identity.Role role, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                // Find role
                var r = await _roleRepository.GetRoleAsync(role.Id, cancellationToken);
                if (r == null)
                    return RequestResult<RoleResult>.Fail("Not Found Role");
                // Update data
                r.Description = role.Description;
                r.Name = role.Name;
                r.NormalizedName = role.Name.ToUpper();
                r.Status = role.Status;
                r.RolePermissions.Clear();
                r.RolePermissions = role.RolePermissions;
                await _roleRepository.UpdateAsync(r, cancellationToken);
                var result = await _roleRepository.SaveChangesAsync(cancellationToken);
                if (result > 0)
                {
                    return RequestResult<RoleResult>.Succeed();
                }
                
                return RequestResult<RoleResult>.Fail("Fail");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<RequestResult<ViewRoleResponse>> ViewRoleAsync(long roleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                // Find role
                var role = await _roleRepository.GetRoleToViewDetail(roleId, cancellationToken);
                if (role == null)
                    return RequestResult<ViewRoleResponse>.Fail("Not Found Role");
                return RequestResult<ViewRoleResponse>.Succeed(data: _mapper.Map<ViewRoleResponse>(role));
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<RequestResult<OffsetPaginationResponse<ViewRoleResponse>>> ViewListRolesAsync(ViewListRolesQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var queryable = await _roleRepository.SearchRoleByName(query, cancellationToken);
                var response = await queryable.PaginateAsync<Domain.Entities.Identity.Role,ViewRoleResponse>(query, cancellationToken);

                return RequestResult<OffsetPaginationResponse<ViewRoleResponse>>.Succeed(data: _mapper.Map<OffsetPaginationResponse<ViewRoleResponse>>(response));
            }
            catch (Exception e)
            {

                throw;
            }
        }

        /// <inheritdoc />
        public async Task<RequestResult<RoleResult>> DeleteRoleAsync(long roleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                // Find role
                var role = await _roleRepository.GetRoleAsync(roleId, cancellationToken);
                if (role == null)
                    return RequestResult<RoleResult>.Fail("Not Found Role");
                if (role.Status == RoleStatus.Deleted)
                    return RequestResult<RoleResult>.Fail("Role is already deleted");
                // Update data
                role.Status = RoleStatus.Deleted;
                await _roleRepository.UpdateAsync(role, cancellationToken);
                var result = await _roleRepository.SaveChangesAsync(cancellationToken);
                if (result > 0)
                {
                    return RequestResult<RoleResult>.Succeed();
                }
                
                return RequestResult<RoleResult>.Fail("Fail");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<RequestResult<RoleResult>> ActivateRoleAsync(long roleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                // Find role
                var role = await _roleRepository.GetRoleAsync(roleId, cancellationToken);
                if (role == null)
                    return RequestResult<RoleResult>.Fail("Not Found Role");
                if (role.Status == RoleStatus.Active)
                    return RequestResult<RoleResult>.Fail("Role is already activated");
                // Update data
                role.Status = RoleStatus.Active;
                await _roleRepository.UpdateAsync(role, cancellationToken);
                var result = await _roleRepository.SaveChangesAsync(cancellationToken);
                if (result > 0)
                {
                    return RequestResult<RoleResult>.Succeed();
                }
                
                return RequestResult<RoleResult>.Fail("Fail");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<RequestResult<RoleResult>> DeactivateRoleAsync(long roleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                // Find role
                var role = await _roleRepository.GetRoleAsync(roleId, cancellationToken);
                if (role == null)
                    return RequestResult<RoleResult>.Fail("Not Found Role");
                if (role.Status == RoleStatus.Inactive)
                    return RequestResult<RoleResult>.Fail("Role is already deactivated");
                // Update data
                role.Status = RoleStatus.Inactive;
                await _roleRepository.UpdateAsync(role, cancellationToken);
                var result = await _roleRepository.SaveChangesAsync(cancellationToken);
                if (result > 0)
                {
                    return RequestResult<RoleResult>.Succeed();
                }
                
                return RequestResult<RoleResult>.Fail("Fail");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}