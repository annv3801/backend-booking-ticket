using Application.DataTransferObjects.Food.Responses;
using Application.Queries.Food;
using Application.Repositories.Food;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Infrastructure.Handlers.Queries.Food;

public class FoodQueryHandler :
    IRequestHandler<GetFoodByIdQuery, FoodResponse?>, 
    IRequestHandler<GetListFoodsQuery, OffsetPaginationResponse<FoodResponse>>,
    IRequestHandler<CheckDuplicatedFoodByNameAndIdQuery, bool>,
    IRequestHandler<CheckDuplicatedFoodByNameQuery, bool>
{
    private readonly IFoodRepository _foodRepository;

    public FoodQueryHandler(IFoodRepository foodRepository)
    {
        _foodRepository = foodRepository;
    }

    public async Task<FoodResponse?> Handle(GetFoodByIdQuery request, CancellationToken cancellationToken)
    {
        return await _foodRepository.GetFoodByIdAsync(request.Id, cancellationToken);
    }
    
    public async Task<OffsetPaginationResponse<FoodResponse>> Handle(GetListFoodsQuery request, CancellationToken cancellationToken)
    {
        return await _foodRepository.GetListFoodsAsync(request.Request, cancellationToken);
    }
    
    public async Task<bool> Handle(CheckDuplicatedFoodByNameAndIdQuery request, CancellationToken cancellationToken)
    {
        return await _foodRepository.IsDuplicatedFoodByNameAndIdAsync(request.Title, request.Id, cancellationToken);
    }
    
    public async Task<bool> Handle(CheckDuplicatedFoodByNameQuery request, CancellationToken cancellationToken)
    {
        return await _foodRepository.IsDuplicatedFoodByNameAsync(request.Title, cancellationToken);
    }
}