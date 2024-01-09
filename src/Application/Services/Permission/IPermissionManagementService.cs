using Application.Common;
using Application.DataTransferObjects.Permission.Responses;
using Application.Queries.Permission;
using Domain.Common.Pagination.OffsetBased;
using Nobi.Core.Responses;

namespace Application.Services.Permission
{
    public interface IPermissionManagementService
    {
        /// <summary>
        /// Update permission
        /// </summary>
        /// <param name="permission"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<RequestResult<PermissionResult>> UpdatePermissionAsync(Domain.Entities.Identity.Permission permission, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// View detailed permission
        /// </summary>
        /// <param name="permId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<RequestResult<ViewPermissionResponse>> ViewPermissionAsync(long permId, CancellationToken cancellationToken = default(CancellationToken));

        Task<RequestResult<OffsetPaginationResponse<ViewPermissionResponse>>> ViewListPermissionsAsync(ViewListPermissionsQuery query, CancellationToken cancellationToken = default(CancellationToken));
    }
}