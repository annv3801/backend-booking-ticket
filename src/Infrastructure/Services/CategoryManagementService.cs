using Application.Commands.Category;
using Application.Common.Interfaces;
using Application.DataTransferObjects.Category.Requests;
using Application.DataTransferObjects.Category.Responses;
using Application.Interface;
using Application.Queries.Category;
using Application.Repositories.Category;
using Application.Services.Category;
using AutoMapper;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nobi.Core.Responses;

// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract


namespace Infrastructure.Services;

public class CategoryManagementService : ICategoryManagementService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ILoggerService _loggerService;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentAccountService _currentAccountService;

    public CategoryManagementService(IMapper mapper, IMediator mediator, ILoggerService loggerService, ICategoryRepository categoryRepository,
        IDateTimeService dateTimeService, ICurrentAccountService currentAccountService)
    {
        _mapper = mapper;
        _mediator = mediator;
        _loggerService = loggerService;
        _categoryRepository = categoryRepository;
        _dateTimeService = dateTimeService;
        _currentAccountService = currentAccountService;
    }

    public async Task<RequestResult<bool>> CreateCategoryAsync(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var parentPath = string.Empty;

            // Check duplicate category name
            if (await _mediator.Send(new CheckDuplicatedCategoryByNameQuery
                {
                    Name = request.Name,
                }, cancellationToken))
                return RequestResult<bool>.Fail("Item is duplicated");

            // Create Category 
            var categoryEntity = _mapper.Map<CategoryEntity>(request);

            categoryEntity.CreatedBy = _currentAccountService.Id;
            categoryEntity.CreatedTime = _dateTimeService.NowUtc;

            var resultCreateCategory = await _mediator.Send(new CreateCategoryCommand {Entity = categoryEntity}, cancellationToken);
            if (resultCreateCategory <= 0)
                return RequestResult<bool>.Fail("Save data failed");
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateCategoryAsync));
            throw;
        }
    }

    public async Task<RequestResult<bool>> UpdateCategoryAsync(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var parentPath = string.Empty;
            // Check duplicate category name
            if (await _mediator.Send(new CheckDuplicatedCategoryByNameAndIdQuery
                {
                    Name = request.Name,
                    Id = request.Id,
                }, cancellationToken))
                return RequestResult<bool>.Fail("Item is duplicated");


            var existedCategory = await _categoryRepository.GetCategoryEntityByIdAsync(request.Id, cancellationToken);
            if (existedCategory == null)
                return RequestResult<bool>.Fail("Category is not found");

            
            // Update value to existed Category
            existedCategory.Name = request.Name;

            var resultUpdateCategory = await _mediator.Send(new UpdateCategoryCommand
            {
                Request = existedCategory,
            }, cancellationToken);
            if (resultUpdateCategory <= 0)
                return RequestResult<bool>.Fail("Save data failed");
           
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(UpdateCategoryAsync));
            throw;
        }
    }

    public async Task<RequestResult<bool>> DeleteCategoryAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            // Check for existence
            var categoryToDelete = await _categoryRepository.GetCategoryByIdAsync(id, cancellationToken);
            if (categoryToDelete == null)
                return RequestResult<bool>.Fail("Category is not found");


            var resultDeleteCategory = await _mediator.Send(new DeleteCategoryCommand
            {
                Id = id,
            }, cancellationToken);
            if (resultDeleteCategory <= 0)
                return RequestResult<bool>.Fail("Save data failed");
           
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(DeleteCategoryAsync));
            throw;
        }
    }

    public async Task<RequestResult<CategoryResponse>> GetCategoryAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery
            {
                Id = id,
            }, cancellationToken);
            if (category == null)
                return RequestResult<CategoryResponse>.Fail("Category is not found");

            return RequestResult<CategoryResponse>.Succeed(null, _mapper.Map<CategoryResponse>(category));
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetCategoryAsync));
            throw;
        }
    }

    public async Task<RequestResult<OffsetPaginationResponse<CategoryResponse>>> GetListCategoriesAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // check tenant valid
            var category = await _mediator.Send(new GetListCategoriesQuery
            {
                OffsetPaginationRequest = request,
            }, cancellationToken);

            return RequestResult<OffsetPaginationResponse<CategoryResponse>>.Succeed(null, category);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetListCategoriesAsync));
            throw;
        }
    }

    public async Task<RequestResult<ICollection<CategoryResponse>>> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        var query = _categoryRepository.Entity.Select(x => new CategoryResponse()
        {
            Name = x.Name,
            Status = x.Status,
            Id = x.Id,
        });


        var response = await query.ToListAsync(cancellationToken);
        return RequestResult<ICollection<CategoryResponse>>.Succeed(data: response);
    }
}