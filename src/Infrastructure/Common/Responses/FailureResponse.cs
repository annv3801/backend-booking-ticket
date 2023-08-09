using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;

namespace Infrastructure.Common.Responses;
[ExcludeFromCodeCoverage]
public class FailureResponse
{
    [DefaultValue(202)] public int Status { get; set; } = 202;
    [DefaultValue("")]
    public IEnumerable<ErrorItem> Errors { get; set; }
    public FailureResponse(IEnumerable<ErrorItem> errors)
    {
        Errors = errors;
    }
}
