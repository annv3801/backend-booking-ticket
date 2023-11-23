using Application.DataTransferObjects.Food.Requests;
using Application.DataTransferObjects.Food.Responses;
using Domain.Common.Pagination.OffsetBased;
using Nobi.Core.Responses;

namespace Application.Services.Food;

public interface IFoodManagementService
{
    Task<RequestResult<bool>> CreateFoodAsync(CreateFoodRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> UpdateFoodAsync(UpdateFoodRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> DeleteFoodAsync(long id, CancellationToken cancellationToken);
    Task<RequestResult<FoodResponse>> GetFoodAsync(long id, CancellationToken cancellationToken);
    Task<RequestResult<OffsetPaginationResponse<FoodResponse>>> GetListFoodsAsync(ViewFoodRequest request, CancellationToken cancellationToken);
}