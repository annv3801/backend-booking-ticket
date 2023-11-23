using System.Diagnostics.CodeAnalysis;
using Domain.Common.Pagination.OffsetBased;

namespace Application.DataTransferObjects.Group.Requests;

[ExcludeFromCodeCoverage]
public class ViewGroupRequest
{
    public OffsetPaginationRequest Request { get; set; }
    public string Type { get; set; }
}