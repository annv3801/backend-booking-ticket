using Application.DataTransferObjects.Food.Requests;
using Application.DataTransferObjects.Food.Responses;
using Application.DataTransferObjects.Group.Requests;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Application.Queries.Food;

public class GetListFoodsQuery : IRequest<OffsetPaginationResponse<FoodResponse>>
{
    public ViewFoodRequest Request { get; set; }
}