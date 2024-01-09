using Application.Commands.Permission;
using Application.DataTransferObjects.Permission.Requests;
using Application.DataTransferObjects.Permission.Responses;
using Application.Queries.Permission;
using AutoMapper;

namespace Infrastructure.Profiles
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<UpdatePermissionCommand, Domain.Entities.Identity.Permission>()
                .ForMember(d => d.Id, o => o.MapFrom(src => src.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
                .ForMember(d => d.NormalizedName, o => o.MapFrom(s => s.Name.ToUpper()));

            CreateMap<Domain.Entities.Identity.Permission, ViewPermissionResponse>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name));

            CreateMap<ViewListPermissionsRequest, ViewListPermissionsQuery>();
        }
    }
}