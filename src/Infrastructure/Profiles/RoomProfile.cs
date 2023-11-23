using Application.DataTransferObjects.Room.Requests;
using Application.DataTransferObjects.Room.Responses;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Profiles;

public class RoomProfile: Profile
{
    public RoomProfile()
    {
        CreateMap<RoomEntity, RoomResponse>();
        CreateMap<CreateRoomRequest, RoomEntity>();
    }
}