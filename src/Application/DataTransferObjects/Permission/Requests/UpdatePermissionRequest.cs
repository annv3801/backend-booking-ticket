using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.Permission.Requests
{
    [ExcludeFromCodeCoverage]
    public class UpdatePermissionRequest
    {
        public string Name { get; set; } = "";
        public string? Description { get; set; }
    }
}