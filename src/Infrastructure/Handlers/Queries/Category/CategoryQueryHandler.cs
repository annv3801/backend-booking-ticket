using Application.DataTransferObjects.Category.Responses;
using Application.Queries.Category;
using Application.Repositories.Category;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Infrastructure.Handlers.Queries.Category;

public class CategoryQueryHandler :
    IRequestHandler<GetCategoryByIdQuery, CategoryResponse?>, 
    IRequestHandler<GetListCategoriesQuery, OffsetPaginationResponse<CategoryResponse>>,
    IRequestHandler<CheckDuplicatedCategoryByNameAndIdQuery, bool>,
    IRequestHandler<CheckDuplicatedCategoryByNameQuery, bool>
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryQueryHandler(ICategoryRepository categoryReadOnlyRepository)
    {
        _categoryRepository = categoryReadOnlyRepository;
    }

    public async Task<CategoryResponse?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await _categoryRepository.GetCategoryByIdAsync(request.Id, cancellationToken);
    }
    
    public async Task<OffsetPaginationResponse<CategoryResponse>> Handle(GetListCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _categoryRepository.GetListCategoriesAsync(request.OffsetPaginationRequest, cancellationToken);
    }
    
    public async Task<bool> Handle(CheckDuplicatedCategoryByNameAndIdQuery request, CancellationToken cancellationToken)
    {
        return await _categoryRepository.IsDuplicatedCategoryByNameAndIdAsync(request.Name, request.Id, cancellationToken);
    }
    
    public async Task<bool> Handle(CheckDuplicatedCategoryByNameQuery request, CancellationToken cancellationToken)
    {
        return await _categoryRepository.IsDuplicatedCategoryByNameAsync(request.Name, cancellationToken);
    }
}