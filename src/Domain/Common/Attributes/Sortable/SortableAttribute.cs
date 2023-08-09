using System.Diagnostics.CodeAnalysis;

namespace Domain.Common.Attributes.Sortable;
[ExcludeFromCodeCoverage]
public class SortableAttribute : Attribute
{
    /// <summary>
    /// Name of sortable field
    /// </summary>
    public string OrderBy { get; set; }
}
