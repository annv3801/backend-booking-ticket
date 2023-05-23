using Application.Common;
using Application.DataTransferObjects.Room.Responses;
using AutoMapper;

namespace Infrastructure.Profiles.Room;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<Domain.Entities.Room, ViewRoomResponse>().ReverseMap();
        CreateMap<Domain.Entities.Room, RoomResult>().ReverseMap();
    }
}