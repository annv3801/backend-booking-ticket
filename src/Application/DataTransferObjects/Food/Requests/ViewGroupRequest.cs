using System.Diagnostics.CodeAnalysis;
using Domain.Common.Pagination.OffsetBased;

namespace Application.DataTransferObjects.Food.Requests;

[ExcludeFromCodeCoverage]
public class ViewFoodRequest : OffsetPaginationRequest
{
    public long GroupId { get; set; }
}