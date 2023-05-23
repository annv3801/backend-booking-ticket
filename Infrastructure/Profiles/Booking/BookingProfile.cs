using Application.Commands.Booking;
using Application.Common;
using Application.DataTransferObjects.Booking.Requests;
using AutoMapper;

namespace Infrastructure.Profiles.Booking;
public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<BookingRequest, Domain.Entities.Booking>().ReverseMap();
        CreateMap<BookingRequest, BookingCommand>();
        CreateMap<Domain.Entities.Booking, BookingResult>();
    }
}
