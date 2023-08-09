using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.Pagination.Responses;
[ExcludeFromCodeCoverage]
public class PaginationBaseResponse<T>
{
    public int CurrentPage { get; set; } = 0;

    public int PageSize { get; set; } = 100;

    public int TotalPages { get; set; } = 0;

    public int TotalItems { get; set; } = 0;

    public string? OrderBy { get; set; } = "";

    public bool OrderByDesc { get; set; } = false;

    public List<T> Result { get; set; } = new List<T>();
}
