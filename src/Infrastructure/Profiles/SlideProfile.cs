using Application.Commands.Slide;
using Application.DataTransferObjects.Slide.Requests;
using Application.DataTransferObjects.Slide.Responses;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Profiles.Account.Resolvers;

namespace Infrastructure.Profiles;

public class SlideProfile : Profile
{
    public SlideProfile()
    {
        // Create
        CreateMap<CreateSlideRequest, CreateSlideCommand>().ReverseMap();
        CreateMap<CreateSlideRequest, SlideEntity>()
            .ForMember(d => d.Id, o => o.MapFrom<IdGeneratorResolver>());

        // Update 
        CreateMap<UpdateSlideRequest, UpdateSlideCommand>().ReverseMap();


        CreateMap<SlideEntity, SlideResponse>()
            ;
    }
}