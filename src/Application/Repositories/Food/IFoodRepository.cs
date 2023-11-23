using Application.DataTransferObjects.Food.Requests;
using Application.DataTransferObjects.Food.Responses;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Entities;

namespace Application.Repositories.Food;
public interface IFoodRepository : IRepository<FoodEntity>
{
    Task<OffsetPaginationResponse<FoodResponse>> GetListFoodsAsync(ViewFoodRequest request, CancellationToken cancellationToken);
    Task<FoodResponse?> GetFoodByIdAsync(long id, CancellationToken cancellationToken);
    Task<FoodEntity?> GetFoodEntityByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> IsDuplicatedFoodByNameAndIdAsync(string name, long id, CancellationToken cancellationToken);
    Task<bool> IsDuplicatedFoodByNameAsync(string name, CancellationToken cancellationToken);
}
