using System;
using System.Diagnostics.CodeAnalysis;
// ReSharper disable All

namespace Application.DTO.RolePermission.Requests
{
    [ExcludeFromCodeCoverage]
    public class CreateRolePermissionRequest
    {
        public long RoleId { get; set; }
        public long PermissionId { get; set; }
    }
}