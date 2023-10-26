namespace Domain.Common.Pagination.CursorBased;

public class CursorPaginationResponse<TDataType>
{
    public bool HasNext { get; set; }
    public bool HasPrevious { get; set; }
    public ICollection<TDataType>? Data { get; set; }
}