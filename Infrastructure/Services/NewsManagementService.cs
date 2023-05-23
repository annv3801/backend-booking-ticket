using Application.Commands.News;
using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DataTransferObjects.Pagination.Responses;
using Application.DataTransferObjects.News.Requests;
using Application.DataTransferObjects.News.Responses;
using Application.Repositories.News;
using Application.Services.News;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Infrastructure.Services;

public class NewsManagementService : INewsManagementService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly INewsRepository _newsRepository;
    private readonly IStringLocalizationService _localizationService;
    public readonly ICurrentAccountService CurrentAccountService;
    private readonly IPaginationService _paginationService;


    public NewsManagementService(IMediator mediator, IMapper mapper, INewsRepository newsRepository, IStringLocalizationService localizationService, ICurrentAccountService currentAccountService, IPaginationService paginationService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _newsRepository = newsRepository;
        _localizationService = localizationService;
        CurrentAccountService = currentAccountService;
        _paginationService = paginationService;
    }
    
    public async Task<Result<NewsResult>> CreateNewsAsync(CreateNewsRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var newField = new News()
            {
                Id = new Guid(),
                Title = request.Title,
                CategoryId = request.CategoryId,
                ImageFile = request.Image,
                Description = request.Description,
                Status = request.Status
            };

            var result = await _mediator.Send(new CreateNewsCommand(newField), cancellationToken);
            return result > 0 ? Result<NewsResult>.Succeed(_mapper.Map<NewsResult>(newField)) : Result<NewsResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<ViewNewsResponse>> ViewNewsAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _newsRepository.GetNewsByIdAsync(id, cancellationToken);
            if (result == null)
                return Result<ViewNewsResponse>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            var newsResult = _mapper.Map<ViewNewsResponse>(result);

            return Result<ViewNewsResponse>.Succeed(newsResult);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<NewsResult>> DeleteNewsAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Check for existence
            var newsToDelete = await _newsRepository.GetNewsByIdAsync(id, cancellationToken);
            if (newsToDelete == null)
                return Result<NewsResult>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            newsToDelete.Status = 0;
            newsToDelete.LastModifiedById = CurrentAccountService.Id;
            newsToDelete.LastModified = DateTime.Now;

            var resultDeleteTicket = await _mediator.Send(new DeleteNewsCommand(newsToDelete), cancellationToken);
            return resultDeleteTicket <= 0 ? Result<NewsResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService)) : Result<NewsResult>.Succeed(_mapper.Map<NewsResult>(newsToDelete));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<NewsResult>> UpdateNewsAsync(Guid id, UpdateNewsRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find News
            var existedNews = await _newsRepository.GetNewsByIdAsync(id, cancellationToken);
            if (existedNews == null)
                return Result<NewsResult>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            existedNews.Title = request.Title;
            existedNews.Description = request.Description;
            existedNews.CategoryId = request.CategoryId;
            existedNews.Status = request.Status;
            existedNews.LastModified = DateTime.Now;
            existedNews.LastModifiedById = CurrentAccountService.Id;

            var resultUpdateNews = await _mediator.Send(new UpdateNewsCommand(existedNews), cancellationToken);
            return resultUpdateNews <= 0 ? Result<NewsResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService)) : Result<NewsResult>.Succeed(_mapper.Map<NewsResult>(existedNews));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<PaginationBaseResponse<ViewNewsResponse>>> ViewListNewsAsync(ViewListNewsRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var source = await _newsRepository.GetListNewsAsync(request, cancellationToken);
            var result = await _paginationService.PaginateAsync(source, request.Page, request.OrderBy, request.OrderByDesc, request.Size, cancellationToken);
            if (result.Result.Count == 0)
            {
                return Result<PaginationBaseResponse<ViewNewsResponse>>.Fail(
                    _localizationService[LocalizationString.Category.FailedToViewList].Value.ToErrors(_localizationService));
            }
            return Result<PaginationBaseResponse<ViewNewsResponse>>.Succeed(new PaginationBaseResponse<ViewNewsResponse>()
            {
                CurrentPage = result.CurrentPage,
                OrderBy = result.OrderBy,
                OrderByDesc = result.OrderByDesc,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                Result = result.Result.Select(a => new ViewNewsResponse()
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    CategoryId = a.CategoryId,
                    Image = a.ImageFile,
                    Status = a.Status
                }).ToList()
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}