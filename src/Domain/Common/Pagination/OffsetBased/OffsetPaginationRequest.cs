// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using Domain.Common.Pagination.Sorting;

namespace Domain.Common.Pagination.OffsetBased;

public class OffsetPaginationRequest
{
    public int PageSize { get; set; } = 30;
    public int CurrentPage { get; set; } = 1;
    public ICollection<SearchModel>? SearchByFields { get; set; }
    public ICollection<SortModel>? SortByFields { get; set; }
}