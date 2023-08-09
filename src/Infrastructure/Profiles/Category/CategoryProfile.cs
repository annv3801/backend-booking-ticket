using Application.Common;
using Application.DataTransferObjects.Category.Responses;
using AutoMapper;

namespace Infrastructure.Profiles.Category;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Domain.Entities.Category, ViewCategoryResponse>().ReverseMap();
        CreateMap<Domain.Entities.Category, CategoryResult>().ReverseMap();
    }
}