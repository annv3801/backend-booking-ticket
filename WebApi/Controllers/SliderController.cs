using Application.DataTransferObjects.Slider.Requests;
using Application.Services.Slider;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
public class SliderController : Controller
{
    private readonly ISliderManagementService _sliderManagementService;

    public SliderController(ISliderManagementService sliderManagementService)
    {
        _sliderManagementService = sliderManagementService;
    }
    
    [HttpPost]
    [Route("create-slider")]
    [Produces("application/json")]

    public async Task<IActionResult> CreateSliderAsync([FromForm]CreateSliderRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _sliderManagementService.CreateSliderAsync(request, cancellationToken);
            if (!result.Succeeded) return Accepted(result);
            if (result.Data != null)
                return Ok(result);
            return Accepted(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpGet]
    [Route("view-slider/{id}")]
    public async Task<IActionResult> ViewSliderAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _sliderManagementService.ViewSliderAsync(id, cancellationToken);
            if (!result.Succeeded) return Accepted(result);
            if (result.Data != null)
                return Ok(result);
            return Accepted(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    [HttpGet]
    [Route("view-list-slider")]
    // [Cached]
    public async Task<IActionResult>? ViewListSlidersAsync([FromQuery] ViewListSlidersRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _sliderManagementService.ViewListSlidersAsync(request, cancellationToken);
            if (!result.Succeeded) return Accepted(result);
            if (result.Data != null)
                return Ok(result);
            return Accepted(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}