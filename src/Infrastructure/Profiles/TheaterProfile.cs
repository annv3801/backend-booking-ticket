using Application.DataTransferObjects.Theater.Requests;
using Application.DataTransferObjects.Theater.Responses;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Profiles;

public class TheaterProfile: Profile
{
    public TheaterProfile()
    {
        CreateMap<TheaterEntity, TheaterResponse>();
        CreateMap<CreateTheaterRequest, TheaterEntity>();
    }
}