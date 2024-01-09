using Application.Commands.News;
using Application.Common.Interfaces;
using Application.DataTransferObjects.News.Requests;
using Application.DataTransferObjects.News.Responses;
using Application.Interface;
using Application.Queries.News;
using Application.Repositories.News;
using Application.Services.News;
using AutoMapper;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Domain.Entities;
using MediatR;
using Nobi.Core.Responses;

// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract


namespace Infrastructure.Services;

public class NewsManagementService : INewsManagementService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ILoggerService _loggerService;
    private readonly INewsRepository _newsRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly IFileService _fileService;
    public NewsManagementService(IMapper mapper, IMediator mediator, ILoggerService loggerService, INewsRepository newsRepository,
        IDateTimeService dateTimeService, ICurrentAccountService currentAccountService, IFileService fileService)
    {
        _mapper = mapper;
        _mediator = mediator;
        _loggerService = loggerService;
        _newsRepository = newsRepository;
        _dateTimeService = dateTimeService;
        _currentAccountService = currentAccountService;
        _fileService = fileService;
    }

    public async Task<RequestResult<bool>> CreateNewsAsync(CreateNewsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // Check duplicate News name
            if (await _mediator.Send(new CheckDuplicatedNewsByNameQuery
                {
                    Title = request.Title,
                }, cancellationToken))
                return RequestResult<bool>.Fail("Item is duplicated");

            var image = "";
            if (request.Image != null)
            {
                var fileResult = _fileService.SaveImage(request.Image);
                if (fileResult.Item1 == 1)
                {
                    image = fileResult.Item2; // getting name of image
                }
            }
            
            // Create News 
            var newsEntity = _mapper.Map<NewsEntity>(request);

            newsEntity.CreatedBy = _currentAccountService.Id;
            newsEntity.CreatedTime = _dateTimeService.NowUtc;
            newsEntity.Image = image;

            var resultCreateNews = await _mediator.Send(new CreateNewsCommand {Entity = newsEntity}, cancellationToken);
            if (resultCreateNews <= 0)
                return RequestResult<bool>.Fail("Save data failed");
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateNewsAsync));
            throw;
        }
    }

    public async Task<RequestResult<bool>> UpdateNewsAsync(UpdateNewsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // Check duplicate News name
            if (await _mediator.Send(new CheckDuplicatedNewsByNameAndIdQuery
                {
                    Title = request.Title,
                    Id = request.Id,
                }, cancellationToken))
                return RequestResult<bool>.Fail("Item is duplicated");
            
            var existedNews = await _newsRepository.GetNewsEntityByIdAsync(request.Id, cancellationToken);
            if (existedNews == null)
                return RequestResult<bool>.Fail("News is not found");
            
            var currentImage = existedNews.Image; // Load current image name/path from the database or wherever you store it
            var image = "";

            if (request.Image != null)
            {
                // Delete the current image if it exists
                if (!string.IsNullOrEmpty(currentImage))
                {
                    _fileService.DeleteImage(currentImage);
                }

                // Save the new image
                var fileResult = _fileService.SaveImage(request.Image);
                if (fileResult.Item1 == 1)
                {
                    image = fileResult.Item2; // getting name of new image

                    // Save this new image name/path to the database or wherever you store it
                }
            }
            
            // Update value to existed News
            existedNews.Title = request.Title;
            existedNews.Description = request.Description;
            existedNews.Content = request.Content;
            if(image != "") 
                existedNews.Image = image;

            var resultUpdateNews = await _mediator.Send(new UpdateNewsCommand
            {
                Request = existedNews,
            }, cancellationToken);
            if (resultUpdateNews <= 0)
                return RequestResult<bool>.Fail("Save data failed");
           
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(UpdateNewsAsync));
            throw;
        }
    }

    public async Task<RequestResult<bool>> DeleteNewsAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            // Check for existence
            var newsToDelete = await _newsRepository.GetNewsByIdAsync(id, cancellationToken);
            if (newsToDelete == null)
                return RequestResult<bool>.Fail("News is not found");


            var resultDeleteNews = await _mediator.Send(new DeleteNewsCommand
            {
                Id = id,
            }, cancellationToken);
            if (resultDeleteNews <= 0)
                return RequestResult<bool>.Fail("Save data failed");
           
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(DeleteNewsAsync));
            throw;
        }
    }

    public async Task<RequestResult<NewsResponse>> GetNewsAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var news = await _mediator.Send(new GetNewsByIdQuery
            {
                Id = id,
            }, cancellationToken);
            if (news == null)
                return RequestResult<NewsResponse>.Fail("News is not found");

            return RequestResult<NewsResponse>.Succeed(null, _mapper.Map<NewsResponse>(news));
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetNewsAsync));
            throw;
        }
    }

    public async Task<RequestResult<OffsetPaginationResponse<NewsResponse>>> GetListNewsAsync(ViewNewsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // check tenant valid
            var news = await _mediator.Send(new GetListNewsQuery
            {
                Request = request,
            }, cancellationToken);

            return RequestResult<OffsetPaginationResponse<NewsResponse>>.Succeed(null, news);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetListNewsAsync));
            throw;
        }
    }

}