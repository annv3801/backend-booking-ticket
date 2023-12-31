using System.Diagnostics.CodeAnalysis;
using Domain.Common.Pagination.OffsetBased;

namespace Application.DataTransferObjects.Film.Requests;
[ExcludeFromCodeCoverage]
public class ViewListFilmsByGroupRequest : OffsetPaginationRequest
{
    public long GroupId { get; set; }
    public long? AccountId { get; set; }
}
