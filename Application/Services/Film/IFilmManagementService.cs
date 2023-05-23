using Application.Common;
using Application.Common.Models;
using Application.DataTransferObjects.Film.Requests;
using Application.DataTransferObjects.Film.Responses;
using Application.DataTransferObjects.Pagination.Responses;

namespace Application.Services.Film;
public interface IFilmManagementService
{
    Task<Result<FilmResult>> CreateFilmAsync(CreateFilmRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewFilmResponse>> ViewFilmAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<FilmResult>> DeleteFilmAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<FilmResult>> UpdateFilmAsync(Guid id, UpdateFilmRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<PaginationBaseResponse<ViewFilmResponse>>> ViewListFilmsAsync(ViewListFilmsRequest request, CancellationToken cancellationToken = default(CancellationToken));

}
