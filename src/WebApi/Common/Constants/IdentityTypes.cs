using System.Diagnostics.CodeAnalysis;

#pragma warning disable 1591
namespace WebApi.Common.Constants
{
    /// <summary>
    /// System's Identity constant
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class IdentityType
    {
        public const string Permission = "Permission";
        public const string Role = "Role";
        public const string Scope = "Scope";
    }
}