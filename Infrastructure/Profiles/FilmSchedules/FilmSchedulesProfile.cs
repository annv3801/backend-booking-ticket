using Application.Common;
using Application.DataTransferObjects.FilmSchedules.Responses;
using AutoMapper;

namespace Infrastructure.Profiles.FilmSchedules;

public class FilmSchedulesProfile : Profile
{
    public FilmSchedulesProfile()
    {
        CreateMap<Domain.Entities.FilmSchedule, ViewFilmSchedulesResponse>().ReverseMap();
        CreateMap<Domain.Entities.FilmSchedule, FilmSchedulesResult>().ReverseMap();
    }
}