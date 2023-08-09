using Application.Common;
using Application.DataTransferObjects.FilmSchedules.Requests;
using Application.DataTransferObjects.FilmSchedules.Responses;
using Application.Queries.FilmSchedules;
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