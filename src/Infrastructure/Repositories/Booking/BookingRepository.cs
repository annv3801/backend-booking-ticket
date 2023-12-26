using Application.Common.Interfaces;
using Application.DataTransferObjects.Booking.Requests;
using Application.DataTransferObjects.Booking.Responses;
using Application.DataTransferObjects.Food.Responses;
using Application.DataTransferObjects.Seat.Responses;
using Application.Interface;
using Application.Repositories.Booking;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Constants;
using Domain.Entities;
using Domain.Extensions;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Infrastructure.Repositories.Booking;

public class BookingRepository : Repository<BookingEntity, ApplicationDbContext>, IBookingRepository
{
    private readonly DbSet<BookingDetailEntity> _bookingDetailEntities;
    private readonly DbSet<BookingEntity> _bookingEntities;

    public BookingRepository(ApplicationDbContext applicationDbContext, ISnowflakeIdService snowflakeIdService) : base(applicationDbContext, snowflakeIdService)
    {
        _bookingDetailEntities = applicationDbContext.Set<BookingDetailEntity>();
        _bookingEntities = applicationDbContext.Set<BookingEntity>();
    }

    public async Task<OffsetPaginationResponse<BookingResponse>> GetListBookingsAsync(ViewListBookingsRequest request, CancellationToken cancellationToken)
    { 
        IQueryable<BookingResponse> query = null;
        
        if (request.AccountId == 0)
        {
            var booking = _bookingEntities.Where(x => !x.Deleted && x.AccountId == request.AccountId);
            query = booking.Join(_bookingDetailEntities.Include(x => x.Seat), x => x.Id, y => y.BookingId, (x, y) => new
            {
                BookingId = x.Id,
                AccountId = x.AccountId,
                Total = x.Total,
                TotalBeforeDiscount = x.TotalBeforeDiscount,
                x.Discount,
                x.CouponId,
                x.PaymentMethod,
                x.Status,
                IsReceived = x.IsReceived,
                x.CreatedBy,
                x.CreatedTime,
                Seat = new SeatResponse()
                {
                    Id = y.Seat.Id,
                    Scheduler = y.Seat.Scheduler,
                    Status = y.Seat.Status,
                    RoomSeat = y.Seat.RoomSeat,
                    SchedulerId = y.Seat.SchedulerId,
                    RoomsSeatId = y.Seat.RoomSeatId,
                    FilmName = y.Seat.Scheduler.Film.Name,
                    RoomName = y.Seat.Scheduler.Room.Name,
                    TheaterName = y.Seat.Scheduler.Theater.Name,
                    FilmImage = y.Seat.Scheduler.Film.Image
                },
                Foods = y.Foods
            })
            .GroupBy(x => x.BookingId)
            .Select(group => new BookingResponse()
            {
                Id = group.Key,
                AccountId = group.First().AccountId,
                Total = group.First().Total,
                TotalBeforeDiscount = group.First().TotalBeforeDiscount,
                Discount = group.First().Discount,
                CouponId = group.First().CouponId,
                PaymentMethod = group.First().PaymentMethod,
                Status = group.First().Status,
                IsReceived = group.First().IsReceived,
                CreatedBy = group.First().CreatedBy,
                CreatedTime = group.First().CreatedTime,
                Seats = group.Select(x => x.Seat).ToList(),
                Foods = group.Select(x => x.Foods).First()
            });
        }
        else
        {
            var booking = _bookingEntities.Where(x => !x.Deleted);
            query = booking.Join(_bookingDetailEntities.Include(x => x.Seat), x => x.Id, y => y.BookingId, (x, y) => new
            {
                BookingId = x.Id,
                AccountId = x.AccountId,
                Total = x.Total,
                TotalBeforeDiscount = x.TotalBeforeDiscount,
                x.Discount,
                x.CouponId,
                x.PaymentMethod,
                x.Status,
                IsReceived = x.IsReceived,
                x.CreatedBy,
                x.CreatedTime,
                Seat = new SeatResponse()
                {
                    Id = y.Seat.Id,
                    Scheduler = y.Seat.Scheduler,
                    Status = y.Seat.Status,
                    RoomSeat = y.Seat.RoomSeat,
                    SchedulerId = y.Seat.SchedulerId,
                    RoomsSeatId = y.Seat.RoomSeatId,
                    FilmName = y.Seat.Scheduler.Film.Name,
                    RoomName = y.Seat.Scheduler.Room.Name,
                    TheaterName = y.Seat.Scheduler.Theater.Name,
                    FilmImage = y.Seat.Scheduler.Film.Image
                },
                Foods = y.Foods
            })
            .GroupBy(x => x.BookingId)
            .Select(group => new BookingResponse()
            {
                Id = group.Key,
                AccountId = group.First().AccountId,
                Total = group.First().Total,
                TotalBeforeDiscount = group.First().TotalBeforeDiscount,
                Discount = group.First().Discount,
                CouponId = group.First().CouponId,
                PaymentMethod = group.First().PaymentMethod,
                Status = group.First().Status,
                IsReceived = group.First().IsReceived,
                CreatedBy = group.First().CreatedBy,
                CreatedTime = group.First().CreatedTime,
                Seats = group.Select(x => x.Seat).ToList(),
                Foods = group.Select(x => x.Foods).First()

            });
        }
        
        var response = await query.PaginateAsync<BookingEntity, BookingResponse>(request, cancellationToken);
        return new OffsetPaginationResponse<BookingResponse>()
        {
            Data = response.Data,
            PageSize = response.PageSize,
            Total = response.Total,
            CurrentPage = response.CurrentPage
        };
    }

    public async Task<BookingResponse?> GetBookingByIdAsync(long id, CancellationToken cancellationToken)
    {
        var booking = _bookingEntities.Where(x => x.Id == id);
        return await booking.Join(_bookingDetailEntities.Include(x => x.Seat.Scheduler.Film).Include(x => x.Seat.Scheduler.Theater), x => x.Id, y => y.BookingId, (x, y) => new
            {
                BookingId = x.Id,
                AccountId = x.AccountId,
                Total = x.Total,
                TotalBeforeDiscount = x.TotalBeforeDiscount,
                x.Discount,
                x.CouponId,
                x.PaymentMethod,
                x.Status,
                IsReceived = x.IsReceived,
                x.CreatedBy,
                x.CreatedTime,
                Seat = new SeatResponse()
                {
                    Id = y.Seat.Id,
                    Scheduler = y.Seat.Scheduler,
                    Status = y.Seat.Status,
                    RoomSeat = y.Seat.RoomSeat,
                    SchedulerId = y.Seat.SchedulerId,
                    RoomsSeatId = y.Seat.RoomSeatId,
                    FilmName = y.Seat.Scheduler.Film.Name,
                    RoomName = y.Seat.Scheduler.Room.Name,
                    TheaterName = y.Seat.Scheduler.Theater.Name,
                    FilmImage = y.Seat.Scheduler.Film.Image
                },
                Foods = y.Foods
            })
            .GroupBy(x => x.BookingId)
            .Select(group => new BookingResponse()
            {
                Id = group.Key,
                AccountId = group.First().AccountId,
                Total = group.First().Total,
                TotalBeforeDiscount = group.First().TotalBeforeDiscount,
                Discount = group.First().Discount,
                CouponId = group.First().CouponId,
                PaymentMethod = group.First().PaymentMethod,
                Status = group.First().Status,
                IsReceived = group.First().IsReceived,
                CreatedBy = group.First().CreatedBy,
                CreatedTime = group.First().CreatedTime,
                Seats = group.Select(x => x.Seat).ToList(),
                Foods = group.Select(x => x.Foods).First()

            }).FirstOrDefaultAsync(cancellationToken);
    }
    
    public async Task<BookingEntity?> GetBookingEntityByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _bookingEntities.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
    }
}