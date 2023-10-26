namespace Domain.Common.Pagination.Sorting;

public class SearchModel
{
    public string SearchFieldName { get; set; } = string.Empty;
    public string? SearchValue { get; set; } = string.Empty;

    public override string ToString()
    {
        return SearchFieldName + " = " + SearchValue;
    }
}