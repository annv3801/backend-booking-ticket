using Application.DataTransferObjects.Booking.Requests;
using Application.DataTransferObjects.Booking.Responses;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Profiles;

public class BookingProfile: Profile
{
    public BookingProfile()
    {
        CreateMap<BookingEntity, BookingResponse>();
        CreateMap<CreateBookingRequest, BookingEntity>();
        CreateMap<BookingDetailEntity, BookingResponse>();
    }
}