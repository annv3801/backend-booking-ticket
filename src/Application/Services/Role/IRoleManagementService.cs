using Application.Common;
using Application.DTO.Role.Responses;
using Application.Queries.Role;
using Domain.Common.Pagination.OffsetBased;
using Nobi.Core.Responses;

namespace Application.Services.Role
{
    public interface IRoleManagementService
    {
        /// <summary>
        /// Create Role
        /// </summary>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<RequestResult<RoleResult>> CreateRoleAsync(Domain.Entities.Identity.Role role, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Update Role
        /// </summary>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<RequestResult<RoleResult>> UpdateRoleAsync(Domain.Entities.Identity.Role role, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// View Role
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<RequestResult<ViewRoleResponse>> ViewRoleAsync(long roleId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// View Role
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<RequestResult<OffsetPaginationResponse<ViewRoleResponse>>> ViewListRolesAsync(ViewListRolesQuery query, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete Role
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<RequestResult<RoleResult>> DeleteRoleAsync(long roleId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Activate Role
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<RequestResult<RoleResult>> ActivateRoleAsync(long roleId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deactivate Role
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<RequestResult<RoleResult>> DeactivateRoleAsync(long roleId, CancellationToken cancellationToken = default(CancellationToken));
    }
}