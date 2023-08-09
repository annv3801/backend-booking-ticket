using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Application.Common;
namespace Infrastructure.Common.Responses;
[ExcludeFromCodeCoverage]
public class SuccessResponse
{
    [DefaultValue(200)] public int Status { get; set; } = 200;
    public string Message { get; set; }
    [DefaultValue("")]
    public object Data { get; set; }
    public SuccessResponse(string message = LocalizationString.Common.Success, object data = null!)
    {
        Message = message;
        Data = data;
    }

    public SuccessResponse()
    {
        Message = LocalizationString.Common.Success;
        Data = new { };
    }
}