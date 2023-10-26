// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using Domain.Common.Pagination.Sorting;

namespace Domain.Common.Pagination.CursorBased;

public class CursorPaginationRequest
{
    public int PageSize { get; set; }
    public long? After { get; set; }
    public long? Before { get; set; }
    public ICollection<SearchModel>? SearchByFields { get; set; }
    public ICollection<SortModel>? SortByFields { get; set; }
}