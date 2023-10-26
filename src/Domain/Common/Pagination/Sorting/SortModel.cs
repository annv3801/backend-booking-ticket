namespace Domain.Common.Pagination.Sorting;

public class SortModel
{
    public string ColName { get; set; } = string.Empty;
    public string SortDirection { get; set; } = string.Empty;

    public string PairAsSqlExpression => $"{ColName} {SortDirection}";
}