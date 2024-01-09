using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable All

#pragma warning disable 8618

namespace Application.DTO.Permission.Responses
{
    [ExcludeFromCodeCoverage]
    public class ViewPermissionWithoutAuditResponse
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}