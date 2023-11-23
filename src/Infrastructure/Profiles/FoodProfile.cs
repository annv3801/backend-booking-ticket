using Application.Commands.Food;
using Application.DataTransferObjects.Food.Requests;
using Application.DataTransferObjects.Food.Responses;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Profiles.Account.Resolvers;

namespace Infrastructure.Profiles;

public class FoodProfile : Profile
{
    public FoodProfile()
    {
        // Create
        CreateMap<CreateFoodRequest, CreateFoodCommand>().ReverseMap();
        CreateMap<CreateFoodRequest, FoodEntity>()
            .ForMember(d => d.Id, o => o.MapFrom<IdGeneratorResolver>());

        // Update 
        CreateMap<UpdateFoodRequest, UpdateFoodCommand>().ReverseMap();


        CreateMap<FoodEntity, FoodResponse>()
            ;
    }
}