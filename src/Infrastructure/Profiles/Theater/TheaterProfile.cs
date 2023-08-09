using Application.Common;
using Application.DataTransferObjects.Theater.Responses;
using AutoMapper;

namespace Infrastructure.Profiles.Theater;

public class TheaterProfile : Profile
{
    public TheaterProfile()
    {
        CreateMap<Domain.Entities.Theater, ViewTheaterResponse>().ReverseMap();
        CreateMap<Domain.Entities.Theater, TheaterResult>().ReverseMap();
    }
}