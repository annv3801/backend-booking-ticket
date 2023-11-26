using Application.Commands.RoomSeat;
using Application.DataTransferObjects.RoomSeat.Requests;
using Application.DataTransferObjects.RoomSeat.Responses;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Profiles.Account.Resolvers;

namespace Infrastructure.Profiles;

public class RoomSeatProfile : Profile
{
    public RoomSeatProfile()
    {
        // Create
        CreateMap<CreateRoomSeatRequest, CreateRoomSeatCommand>().ReverseMap();
        CreateMap<CreateRoomSeatRequest, RoomSeatEntity>()
            .ForMember(d => d.Id, o => o.MapFrom<IdGeneratorResolver>());

        // Update 
        CreateMap<UpdateRoomSeatRequest, UpdateRoomSeatCommand>().ReverseMap();


        CreateMap<RoomSeatEntity, RoomSeatResponse>()
            ;
    }
}