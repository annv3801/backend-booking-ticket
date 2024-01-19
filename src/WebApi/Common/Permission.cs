using System.Diagnostics.CodeAnalysis;

#pragma warning disable 1591
namespace WebApi.Common
{
    /// <summary>
    /// Permission format: app:area:permission
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Permission
    {
        public const string Root = "tomoku:root:root";
    }
}