using Application.Commands.Category;
using Application.DataTransferObjects.Category.Requests;
using Application.DataTransferObjects.Category.Responses;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Profiles.Account.Resolvers;

namespace Infrastructure.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        // Create
        CreateMap<CreateCategoryRequest, CreateCategoryCommand>().ReverseMap();
        CreateMap<CreateCategoryRequest, CategoryEntity>()
            .ForMember(d => d.Id, o => o.MapFrom<IdGeneratorResolver>());

        // Update 
        CreateMap<UpdateCategoryRequest, UpdateCategoryCommand>().ReverseMap();


        CreateMap<CategoryEntity, CategoryResponse>()
            ;
    }
}