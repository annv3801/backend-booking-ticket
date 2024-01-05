using Application.DataTransferObjects.Film.Requests;
using Application.DataTransferObjects.Film.Responses;
using Domain.Common.Pagination.OffsetBased;
using Nobi.Core.Responses;

namespace Application.Services.Film;

public interface IFilmManagementService
{
    Task<RequestResult<bool>> CreateFilmAsync(CreateFilmRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> UpdateFilmAsync(UpdateFilmRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> DeleteFilmAsync(long id, CancellationToken cancellationToken);
    Task<RequestResult<FilmResponse>> GetFilmAsync(long id, CancellationToken cancellationToken);   
    Task<RequestResult<OffsetPaginationResponse<FilmResponse>>> GetListFilmsByGroupAsync(ViewListFilmsByGroupRequest request, CancellationToken cancellationToken);
    Task<RequestResult<OffsetPaginationResponse<FilmResponse>>> GetListFilmsAsync(OffsetPaginationRequest request, CancellationToken cancellationToken);
    Task<RequestResult<OffsetPaginationResponse<FilmResponse>>> GetListFilmsFavoritesByAccountAsync(ViewListFilmsFavoriteByAccountRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> CreateFeedbackFilmAsync(CreateFeedbackFilmRequest request, CancellationToken cancellationToken);
}