using Application.DataTransferObjects.Group.Responses;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Entities;

namespace Application.Repositories.Group;
public interface IGroupRepository : IRepository<GroupEntity>
{
    Task<OffsetPaginationResponse<GroupResponse>> GetListGroupsByTypeAsync(OffsetPaginationRequest request, string type, CancellationToken cancellationToken);
    Task<OffsetPaginationResponse<GroupResponse>> GetListGroupsAsync(OffsetPaginationRequest request, CancellationToken cancellationToken);
    Task<GroupResponse?> GetGroupByIdAsync(long id, CancellationToken cancellationToken);
    Task<GroupEntity?> GetGroupEntityByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> IsDuplicatedGroupByNameAndIdAsync(string name, long id, CancellationToken cancellationToken);
    Task<bool> IsDuplicatedGroupByNameAsync(string name, CancellationToken cancellationToken);
}
