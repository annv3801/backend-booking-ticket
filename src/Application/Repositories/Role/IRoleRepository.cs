using Application.DTO.Role.Responses;
using Application.Queries.Role;
using Domain.Common.Repository;

namespace Application.Repositories.Role
{
    public interface IRoleRepository : IRepository<Domain.Entities.Identity.Role>
    {
        /// <summary>
        /// Get role by its name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Domain.Entities.Identity.Role?> GetRoleAsync(string name, CancellationToken cancellationToken = default(CancellationToken));

        Task<Domain.Entities.Identity.Role?> GetRoleAsync(long roleId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get roles by its id
        /// </summary>
        /// <param name="roleIds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Domain.Entities.Identity.Role>?> GetRolesAsync(List<long> roleIds, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get a role with created by, last modified by and list perms
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Domain.Entities.Identity.Role?> GetRoleToViewDetail(long roleId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Search for role by its name
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IQueryable<ViewRoleResponse>> SearchRoleByName(ViewListRolesQuery query, CancellationToken cancellationToken = default(CancellationToken));
    }
}