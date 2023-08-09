using Application.Common;
using Application.DataTransferObjects.Film.Responses;
using AutoMapper;

namespace Infrastructure.Profiles.Film;

public class FilmProfile : Profile
{
    public FilmProfile()
    {
        CreateMap<Domain.Entities.Film, ViewFilmResponse>().ReverseMap();
        CreateMap<Domain.Entities.Film, FilmResult>().ReverseMap();
    }
}