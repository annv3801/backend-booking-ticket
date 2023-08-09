using Application.Commands.Category;
using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Requests;
using Application.DataTransferObjects.Category.Requests;
using Application.DataTransferObjects.Category.Responses;
using Application.DataTransferObjects.Pagination.Responses;
using Application.Repositories.Category;
using Application.Services.Category;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Infrastructure.Services;

public class CategoryManagementService : ICategoryManagementService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IStringLocalizationService _localizationService;
    public readonly ICurrentAccountService CurrentAccountService;
    private readonly IPaginationService _paginationService;


    public CategoryManagementService(IMediator mediator, IMapper mapper, ICategoryRepository categoryRepository, IStringLocalizationService localizationService, ICurrentAccountService currentAccountService, IPaginationService paginationService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
        _localizationService = localizationService;
        CurrentAccountService = currentAccountService;
        _paginationService = paginationService;
    }
    
    public async Task<Result<CategoryResult>> CreateCategoryAsync(CreateCategoryRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Check duplicated name or code of field
            var existedCategory = await _categoryRepository.GetCategoryByShortenUrlAsync(request.ShortenUrl, cancellationToken);
            if (existedCategory != null)
                return Result<CategoryResult>.Fail(LocalizationString.Category.DuplicateCategory.ToErrors(_localizationService));

            var newField = new Category()
            {
                Id = new Guid(),
                Name = request.Name,
                ShortenUrl = request.ShortenUrl,
                Status = request.Status
            };

            var result = await _mediator.Send(new CreateCategoryCommand(newField), cancellationToken);
            return result > 0 ? Result<CategoryResult>.Succeed(_mapper.Map<CategoryResult>(newField)) : Result<CategoryResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<ViewCategoryResponse>> ViewCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _categoryRepository.GetCategoryByIdAsync(categoryId, cancellationToken);
            if (result == null)
                return Result<ViewCategoryResponse>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            var categoryResult = _mapper.Map<ViewCategoryResponse>(result);

            return Result<ViewCategoryResponse>.Succeed(categoryResult);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<CategoryResult>> DeleteCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Check for existence
            var categoryToDelete = await _categoryRepository.GetCategoryByIdAsync(categoryId, cancellationToken);
            if (categoryToDelete == null)
                return Result<CategoryResult>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            categoryToDelete.Status = 0;
            categoryToDelete.LastModifiedById = CurrentAccountService.Id;
            categoryToDelete.LastModified = DateTime.Now;

            var resultDeleteRole = await _mediator.Send(new DeleteCategoryCommand(categoryToDelete), cancellationToken);
            return resultDeleteRole <= 0 ? Result<CategoryResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService)) : Result<CategoryResult>.Succeed(_mapper.Map<CategoryResult>(categoryToDelete));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<CategoryResult>> UpdateCategoryAsync(Guid id, UpdateCategoryRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find role
            var existedCategory = await _categoryRepository.GetCategoryByIdAsync(id, cancellationToken);
            if (existedCategory == null)
                return Result<CategoryResult>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            // Check duplicated shorten url
            var categoryValid = await _categoryRepository.GetCategoryByShortenUrlAsync(request.ShortenUrl, cancellationToken);
            if (categoryValid != null)
                return Result<CategoryResult>.Fail(LocalizationString.Category.DuplicateCategory.ToErrors(_localizationService));

            existedCategory.Name = request.Name;
            existedCategory.ShortenUrl = request.ShortenUrl;
            existedCategory.Status = request.Status;
            existedCategory.LastModified = DateTime.Now;
            existedCategory.LastModifiedById = CurrentAccountService.Id;

            var resultUpdateRole = await _mediator.Send(new UpdateCategoryCommand(existedCategory), cancellationToken);
            return resultUpdateRole <= 0 ? Result<CategoryResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService)) : Result<CategoryResult>.Succeed(_mapper.Map<CategoryResult>(existedCategory));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<PaginationBaseResponse<ViewCategoryResponse>>> ViewListCategoriesAsync(ViewListCategoriesRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var source = await _categoryRepository.GetListCategoryAsync(request, cancellationToken);
            var result = await _paginationService.PaginateAsync(source, request.Page, request.OrderBy, request.OrderByDesc, request.Size, cancellationToken);
            if (result.Result.Count == 0)
            {
                return Result<PaginationBaseResponse<ViewCategoryResponse>>.Fail(
                    _localizationService[LocalizationString.Category.FailedToViewList].Value.ToErrors(_localizationService));
            }
            return Result<PaginationBaseResponse<ViewCategoryResponse>>.Succeed(new PaginationBaseResponse<ViewCategoryResponse>()
            {
                CurrentPage = result.CurrentPage,
                OrderBy = result.OrderBy,
                OrderByDesc = result.OrderByDesc,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                Result = result.Result.Select(a => new ViewCategoryResponse()
                {
                    Id = a.Id,
                    Name = a.Name,
                    ShortenUrl = a.ShortenUrl,
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