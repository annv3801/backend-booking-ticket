using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;

#pragma warning disable 8618

namespace Application.DataTransferObjects.Permission.Responses
{
    [ExcludeFromCodeCoverage]
    public class ViewPermissionResponse
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}