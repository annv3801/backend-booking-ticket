using Application.Common;
using Application.Common.Models;
using Application.DataTransferObjects.FilmSchedules.Requests;
using Application.DataTransferObjects.FilmSchedules.Responses;
using Application.DataTransferObjects.Pagination.Responses;
using Application.Queries.FilmSchedules;

namespace Application.Services.FilmSchedules;
public interface IFilmSchedulesManagementService
{
    Task<Result<FilmSchedulesResult>> CreateFilmSchedulesAsync(CreateFilmSchedulesRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewFilmSchedulesResponse>> ViewFilmSchedulesAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<FilmSchedulesResult>> DeleteFilmSchedulesAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<FilmSchedulesResult>> UpdateFilmSchedulesAsync(Guid id, UpdateFilmSchedulesRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<PaginationBaseResponse<ViewFilmSchedulesResponse>>> ViewListFilmSchedulesAsync(ViewListFilmSchedulesRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<List<TheaterScheduleResponse>>> ViewListFilmSchedulesByTimeAsync(ViewListFilmSchedulesByTimeQuery query, CancellationToken cancellationToken = default(CancellationToken));

}
