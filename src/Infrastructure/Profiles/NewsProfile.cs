using Application.Commands.News;
using Application.DataTransferObjects.News.Requests;
using Application.DataTransferObjects.News.Responses;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Profiles.Account.Resolvers;

namespace Infrastructure.Profiles;

public class NewsProfile : Profile
{
    public NewsProfile()
    {
        // Create
        CreateMap<CreateNewsRequest, CreateNewsCommand>().ReverseMap();
        CreateMap<CreateNewsRequest, NewsEntity>()
            .ForMember(d => d.Id, o => o.MapFrom<IdGeneratorResolver>());

        // Update 
        CreateMap<UpdateNewsRequest, UpdateNewsCommand>().ReverseMap();


        CreateMap<NewsEntity, NewsResponse>()
            ;
    }
}