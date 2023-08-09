using Application.Common;
using Application.DataTransferObjects.News.Responses;
using AutoMapper;

namespace Infrastructure.Profiles.News;

public class NewsProfile : Profile
{
    public NewsProfile()
    {
        CreateMap<Domain.Entities.News, ViewNewsResponse>().ReverseMap();
        CreateMap<Domain.Entities.News, NewsResult>().ReverseMap();
    }
}