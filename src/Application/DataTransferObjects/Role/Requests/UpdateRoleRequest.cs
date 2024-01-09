using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

namespace Application.DataTransferObjects.Role.Requests
{
    [ExcludeFromCodeCoverage]
    public class UpdateRoleRequest
    {
        public string Name { get; set; } = "";
        public RoleStatus Status { get; set; } = RoleStatus.Active;
        public string? Description { get; set; } = null;
        public List<long> Permissions { get; set; } = new List<long>();
    }
}