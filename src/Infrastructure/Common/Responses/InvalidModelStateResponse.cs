using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Common.Responses;
[ExcludeFromCodeCoverage]
public class InvalidModelStateResponse
{
    [DefaultValue(400)] public int Status { get; set; } = 400;
    [DefaultValue("")]
    public object Errors { get; set; }
    public InvalidModelStateResponse(object errors)
    {
        Errors = errors;
    }
}
