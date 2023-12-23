using Domain.Constants;
using Microsoft.AspNetCore.Http;

namespace Application.DataTransferObjects.Slide.Responses;

public class SlideResponse 
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public long ObjectId { get; set; }
    public string Status { get; set; } = EntityStatus.Active;
}
