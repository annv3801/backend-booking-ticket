using Application.DataTransferObjects.Theater.Responses;
using Application.Queries.Scheduler;
using Application.Queries.Theater;
using Application.Repositories.Theater;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Infrastructure.Handlers.Queries.Theater;

public class TheaterQueryHandler :
    IRequestHandler<GetTheaterByIdQuery, TheaterResponse?>, 
    IRequestHandler<GetListTheatersQuery, OffsetPaginationResponse<TheaterResponse>>,
    IRequestHandler<CheckDuplicatedTheaterByNameAndIdQuery, bool>,
    IRequestHandler<CheckDuplicatedTheaterByNameQuery, bool>
{
    private readonly ITheaterRepository _theaterRepository;

    public TheaterQueryHandler(ITheaterRepository theaterRepository)
    {
        _theaterRepository = theaterRepository;
    }

    public async Task<TheaterResponse?> Handle(GetTheaterByIdQuery request, CancellationToken cancellationToken)
    {
        return await _theaterRepository.GetTheaterByIdAsync(request.Id, request.AccountId, cancellationToken);
    }
    
    public async Task<OffsetPaginationResponse<TheaterResponse>> Handle(GetListTheatersQuery request, CancellationToken cancellationToken)
    {
        return await _theaterRepository.GetListTheatersAsync(request.OffsetPaginationRequest, request.AccountId, cancellationToken);
    }
    public async Task<bool> Handle(CheckDuplicatedTheaterByNameAndIdQuery request, CancellationToken cancellationToken)
    {
        return await _theaterRepository.IsDuplicatedTheaterByNameAndIdAsync(request.Name, request.Id, cancellationToken);
    }
    
    public async Task<bool> Handle(CheckDuplicatedTheaterByNameQuery request, CancellationToken cancellationToken)
    {
        return await _theaterRepository.IsDuplicatedTheaterByNameAsync(request.Name, cancellationToken);
    }
}