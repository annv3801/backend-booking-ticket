using Application.DataTransferObjects.News.Responses;
using Application.Queries.News;
using Application.Repositories.News;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Infrastructure.Handlers.Queries.News;

public class NewsQueryHandler :
    IRequestHandler<GetNewsByIdQuery, NewsResponse?>, 
    IRequestHandler<GetListNewsQuery, OffsetPaginationResponse<NewsResponse>>,
    IRequestHandler<CheckDuplicatedNewsByNameAndIdQuery, bool>,
    IRequestHandler<CheckDuplicatedNewsByNameQuery, bool>
{
    private readonly INewsRepository _newsRepository;

    public NewsQueryHandler(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    public async Task<NewsResponse?> Handle(GetNewsByIdQuery request, CancellationToken cancellationToken)
    {
        return await _newsRepository.GetNewsByIdAsync(request.Id, cancellationToken);
    }
    
    public async Task<OffsetPaginationResponse<NewsResponse>> Handle(GetListNewsQuery request, CancellationToken cancellationToken)
    {
        return await _newsRepository.GetListNewsAsync(request.Request, cancellationToken);
    }
    
    public async Task<bool> Handle(CheckDuplicatedNewsByNameAndIdQuery request, CancellationToken cancellationToken)
    {
        return await _newsRepository.IsDuplicatedNewsByNameAndIdAsync(request.Title, request.Id, cancellationToken);
    }
    
    public async Task<bool> Handle(CheckDuplicatedNewsByNameQuery request, CancellationToken cancellationToken)
    {
        return await _newsRepository.IsDuplicatedNewsByNameAsync(request.Title, cancellationToken);
    }
}