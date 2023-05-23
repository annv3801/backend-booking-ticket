using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.Theater.Requests;
[ExcludeFromCodeCoverage]
public class UpdateTheaterRequest
{
    public string Name { get; set; }
    public string Address { get; set; }
    public int Status { get; set; }
}
