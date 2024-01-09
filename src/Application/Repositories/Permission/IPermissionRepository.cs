using Application.DataTransferObjects.Permission.Responses;
using Application.Queries.Permission;
using Domain.Common.Repository;

namespace Application.Repositories.Permission
{
    public interface IPermissionRepository : IRepository<Domain.Entities.Identity.Permission>
    {
        /// <summary>
        /// Find active permission by its id
        /// </summary>
        /// <param name="permissionId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Domain.Entities.Identity.Permission?> FindPermissionById(long permissionId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Find permission by id and include the created by and last modified by
        /// </summary>
        /// <param name="permissionId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Domain.Entities.Identity.Permission?> FindPermissionByIdWithAuditableEntity(long permissionId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Search for Permissions by the name
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IQueryable<ViewPermissionResponse>> SearchPermissionByName(ViewListPermissionsQuery query, CancellationToken cancellationToken = default(CancellationToken));

      
    }
}