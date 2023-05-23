using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.Theater.Responses;
[ExcludeFromCodeCoverage]
public class ViewTheaterResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int Status { get; set; }
}
