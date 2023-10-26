using Application.Commands.Group;
using Application.DataTransferObjects.Group.Requests;
using Application.DataTransferObjects.Group.Responses;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Profiles.Account.Resolvers;

namespace Infrastructure.Profiles;

public class GroupProfile : Profile
{
    public GroupProfile()
    {
        // Create
        CreateMap<CreateGroupRequest, CreateGroupCommand>().ReverseMap();
        CreateMap<CreateGroupRequest, GroupEntity>()
            .ForMember(d => d.Id, o => o.MapFrom<IdGeneratorResolver>());

        // Update 
        CreateMap<UpdateGroupRequest, UpdateGroupCommand>().ReverseMap();


        CreateMap<GroupEntity, GroupResponse>()
            ;
    }
}