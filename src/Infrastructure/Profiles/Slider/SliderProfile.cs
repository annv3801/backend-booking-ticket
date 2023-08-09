using Application.Common;
using Application.DataTransferObjects.Slider.Responses;
using AutoMapper;

namespace Infrastructure.Profiles.Slider;

public class SliderProfile : Profile
{
    public SliderProfile()
    {
        CreateMap<Domain.Entities.Slider, ViewSliderResponse>().ReverseMap();
        CreateMap<Domain.Entities.Slider, SliderResult>().ReverseMap();
    }
}