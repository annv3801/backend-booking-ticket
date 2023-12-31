using Application.DataTransferObjects.Theater.Responses;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Entities;

namespace Application.Repositories.Theater;
public interface ITheaterRepository : IRepository<TheaterEntity>
{
    Task<OffsetPaginationResponse<TheaterResponse>> GetListTheatersAsync(OffsetPaginationRequest request, long? accountId, CancellationToken cancellationToken);
    Task<TheaterResponse?> GetTheaterByIdAsync(long id, long? accountId, CancellationToken cancellationToken);
    Task<TheaterEntity?> GetTheaterEntityByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> IsDuplicatedTheaterByNameAndIdAsync(string name, long id, CancellationToken cancellationToken);
    Task<bool> IsDuplicatedTheaterByNameAsync(string name, CancellationToken cancellationToken);
}
