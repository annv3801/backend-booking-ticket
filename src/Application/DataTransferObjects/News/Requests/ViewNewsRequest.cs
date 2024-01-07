using System.Diagnostics.CodeAnalysis;
using Domain.Common.Pagination.OffsetBased;

namespace Application.DataTransferObjects.News.Requests;

[ExcludeFromCodeCoverage]
public class ViewNewsRequest : OffsetPaginationRequest
{
    public long GroupId { get; set; }
}