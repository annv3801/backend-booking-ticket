using System.Diagnostics.CodeAnalysis;
using Application.DataTransferObjects.Pagination.Requests;

namespace Application.DataTransferObjects.Film.Requests;
[ExcludeFromCodeCoverage]
public class ViewListFilmByCategoryRequest : PaginationBaseRequest
{
    public string CategorySlug { get; set; }
}
