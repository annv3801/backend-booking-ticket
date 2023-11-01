using Application.DataTransferObjects.Film.Responses;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Profiles;

public class FilmProfile: Profile
{
    public FilmProfile()
    {
        CreateMap<FilmEntity, FilmResponse>();
    }
}