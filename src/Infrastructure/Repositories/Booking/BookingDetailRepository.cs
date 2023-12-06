using Application.Interface;
using Application.Repositories.Booking;
using AutoMapper;
using Domain.Common.Repository;
using Domain.Entities;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Booking;

public class BookingDetailRepository : Repository<BookingDetailEntity, ApplicationDbContext>, IBookingDetailRepository
{
    private readonly DbSet<BookingDetailEntity> _bookingDetailEntities;
    private readonly IMapper _mapper;

    public BookingDetailRepository(ApplicationDbContext applicationDbContext, IMapper mapper, ISnowflakeIdService snowflakeIdService) : base(applicationDbContext, snowflakeIdService)
    {
        _mapper = mapper;
        _bookingDetailEntities = applicationDbContext.Set<BookingDetailEntity>();
    }
}