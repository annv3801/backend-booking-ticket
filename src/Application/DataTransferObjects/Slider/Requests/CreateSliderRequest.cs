using Microsoft.AspNetCore.Http;

namespace Application.DataTransferObjects.Slider.Requests;

public class CreateSliderRequest
{
    public IFormFile? Image { get; set; }
    public int Status { get; set; }
}