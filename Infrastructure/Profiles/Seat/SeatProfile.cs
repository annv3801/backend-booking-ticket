using Application.Common;
using Application.DataTransferObjects.Seat.Responses;
using AutoMapper;

namespace Infrastructure.Profiles.Seat;

public class SeatProfile : Profile
{
    public SeatProfile()
    {
        CreateMap<Domain.Entities.Seat, ViewSeatResponse>().ReverseMap();
        CreateMap<Domain.Entities.Seat, SeatResult>().ReverseMap();
    }
}