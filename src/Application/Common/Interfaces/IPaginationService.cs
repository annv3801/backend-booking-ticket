using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.DataTransferObjects.Pagination.Responses;

namespace Application.Common.Interfaces;
public interface IPaginationService
{
    Task<PaginationBaseResponse<T>> PaginateAsync<T>(IQueryable<T> source, int page, string? orderBy, bool orderByDesc, int pageSize, CancellationToken cancellationToken = default(CancellationToken)) where T : class;
    Task<PaginationBaseResponse<T>> PaginateWithSortManyFieldAsync<T>(IQueryable<T> source, int page, string? orderBy, bool orderByDesc,bool OrderByDIFOTDesc,bool OrderByQAQCDesc, int pageSize, CancellationToken cancellationToken = default(CancellationToken)) where T : class;
    Task<PaginationBaseResponse<T>> PaginateWithListAsync<T>(IEnumerable<T> source, int page, string? orderBy, bool orderByDesc, int pageSize, CancellationToken cancellationToken = default(CancellationToken)) where T : class;
}
