using Application.Commands.Seat;
using Application.DataTransferObjects.Seat.Requests;
using Application.DataTransferObjects.Seat.Responses;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Profiles.Account.Resolvers;

namespace Infrastructure.Profiles;

public class SeatProfile : Profile
{
    public SeatProfile()
    {
        // Create
        CreateMap<CreateSeatRequest, CreateSeatCommand>().ReverseMap();
        CreateMap<CreateSeatRequest, SeatEntity>()
            .ForMember(d => d.Id, o => o.MapFrom<IdGeneratorResolver>());

        // Update 
        CreateMap<UpdateSeatRequest, UpdateSeatCommand>().ReverseMap();


        CreateMap<SeatEntity, SeatResponse>();
    }
}