using Application.Commands.Role;
using Application.DataTransferObjects.Role.Requests;
using Application.DTO.Permission.Responses;
using Application.DTO.Role.Responses;
using Application.DTO.RolePermission.Requests;
using Application.Queries.Role;
using AutoMapper;
using Domain.Entities.Identity;

namespace Infrastructure.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<CreateRoleRequest, CreateRoleCommand>();
            CreateMap<UpdateRoleRequest, UpdateRoleCommand>();
            CreateMap<CreateRolePermissionRequest, RolePermission>()
                .ForMember(d => d.PermissionId, o => o.MapFrom(s => s.PermissionId))
                .ForMember(d => d.RoleId, o => o.MapFrom(s => s.RoleId));

            CreateMap<UpdateRoleCommand, Domain.Entities.Identity.Role>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.NormalizedName, o => o.MapFrom(s => s.Name.ToUpper()))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status))
                .AfterMap((s, d) => d.RolePermissions = s.Permissions.Select(pid => new RolePermission()
                {
                    PermissionId = pid,
                    RoleId = d.Id
                }).ToList());


            CreateMap<CreateRoleCommand, Domain.Entities.Identity.Role>()
                .ForMember(d => d.Id, o => o.MapFrom(s => Guid.NewGuid()))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.NormalizedName, o => o.MapFrom(s => s.Name.ToUpper()))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status))
                .AfterMap((s, d) => d.RolePermissions = s.Permissions.Select(pid => new RolePermission()
                {
                    PermissionId = pid,
                    RoleId = d.Id
                }).ToList());

            CreateMap<Domain.Entities.Identity.Role, ViewRoleResponse>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.NormalizedName, o => o.MapFrom(s => s.NormalizedName))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status))
                .AfterMap((s, d) => d.Permissions = s.RolePermissions.Select(r => new ViewPermissionWithoutAuditResponse()
                {
                    Id = r.PermissionId,
                    Name = r.Permission?.Name,
                    Code = r.Permission?.Code,
                    Description = r.Permission?.Description
                }).ToList());
            CreateMap<ViewListRolesRequest, ViewListRolesQuery>();
            // CreateMap<PaginationBaseResponse<Domain.Entities.Identity.Role>, PaginationBaseResponse<ViewRoleResponse>>()
            //     .ForMember(d => d.CurrentPage, o => o.MapFrom(s => s.CurrentPage))
            //     .ForMember(d => d.OrderBy, o => o.MapFrom(s => s.OrderBy))
            //     .ForMember(d => d.PageSize, o => o.MapFrom(s => s.PageSize))
            //     .ForMember(d => d.TotalItems, o => o.MapFrom(s => s.TotalItems))
            //     .ForMember(d => d.TotalPages, o => o.MapFrom(s => s.TotalPages))
            //     .ForMember(d => d.OrderByDesc, o => o.MapFrom(s => s.OrderByDesc))
            //     .ForMember(d => d.Result, o => o.MapFrom(s => s.Result));
        }
    }
}