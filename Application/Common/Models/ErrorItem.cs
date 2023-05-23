using System.Diagnostics.CodeAnalysis;

namespace Application.Common.Models;
[ExcludeFromCodeCoverage]
public class ErrorItem
{
    public string FieldName { get; set; }
    public string Error { get; set; }
}
