using Application.DataTransferObjects.Film.Responses;
using Application.Queries.Film;
using Application.Repositories.Film;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Infrastructure.Handlers.Queries.Film;

public class FilmQueryHandler :
    IRequestHandler<GetFilmByIdQuery, FilmResponse?>, 
    IRequestHandler<GetListFilmsByGroupQuery, OffsetPaginationResponse<FilmResponse>>,
    IRequestHandler<GetListFilmsQuery, OffsetPaginationResponse<FilmResponse>>
{    

    private readonly IFilmRepository _filmRepository;
    private readonly IFeedbackFilmRepository _feedbackFilmRepository;

    public FilmQueryHandler(IFilmRepository filmRepository, IFeedbackFilmRepository feedbackFilmRepository)
    {
        _filmRepository = filmRepository;
        _feedbackFilmRepository = feedbackFilmRepository;
    }

    public async Task<FilmResponse?> Handle(GetFilmByIdQuery request, CancellationToken cancellationToken)
    {
        return await _filmRepository.GetFilmByIdAsync(request.Id, cancellationToken);
    }
    
    public async Task<OffsetPaginationResponse<FilmResponse>> Handle(GetListFilmsByGroupQuery request, CancellationToken cancellationToken)
    {
        return await _filmRepository.GetListFilmsByGroupsAsync(request.Request, cancellationToken);
    }
    
    public async Task<OffsetPaginationResponse<FilmResponse>> Handle(GetListFilmsQuery request, CancellationToken cancellationToken)
    {
        return await _filmRepository.GetListFilmsAsync(request.Request, cancellationToken);
    }
}