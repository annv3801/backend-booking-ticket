using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.Pagination.Requests;
[ExcludeFromCodeCoverage]
public abstract class PaginationBaseRequest
{
    public string? Keyword { get; set; }
    public string? OrderBy { get; set; }
    public bool OrderByDesc { get; set; } = false;
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 100;
}
