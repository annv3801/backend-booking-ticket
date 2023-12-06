using Application.DataTransferObjects.Booking.Requests;
using Application.DataTransferObjects.Booking.Responses;
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
    private readonly IMapper _mapper;

    public BookingRepository(ApplicationDbContext applicationDbContext, IMapper mapper, ISnowflakeIdService snowflakeIdService) : base(applicationDbContext, snowflakeIdService)
    {
        _mapper = mapper;
        _bookingDetailEntities = applicationDbContext.Set<BookingDetailEntity>();
        _bookingEntities = applicationDbContext.Set<BookingEntity>();
    }

    public async Task<OffsetPaginationResponse<BookingResponse>> GetListBookingsAsync(ViewListBookingsRequest request, CancellationToken cancellationToken)
    {
        IQueryable<BookingResponse> query = null;
        if (request.AccountId == 0)
        {
            var booking = _bookingEntities.Where(x => !x.Deleted && x.AccountId == request.AccountId);
            query = booking.Join(_bookingDetailEntities, x => x.Id, y => y.BookingId, (x, y) => new BookingResponse()
            {
                Id = x.Id,
                AccountId = x.AccountId,
                Total = x.Total,
                IsReceived = x.IsReceived,
                Seats = new List<SeatResponse>()
                {
                    new SeatResponse()
                    {
                        Id = y.Seat.Id,
                        Scheduler = y.Seat.Scheduler,
                        Status = y.Seat.Status,
                        RoomSeat = y.Seat.RoomSeat,
                        SchedulerId = y.Seat.SchedulerId,
                        RoomsSeatId = y.Seat.RoomSeatId
                    }
                }
            });
        }
        else
        {
            var booking = _bookingEntities.Where(x => !x.Deleted);
            query = booking.Join(_bookingDetailEntities, x => x.Id, y => y.BookingId, (x, y) => new BookingResponse()
            {
                Id = x.Id,
                AccountId = x.AccountId,
                Total = x.Total,
                IsReceived = x.IsReceived,
                Seats = new List<SeatResponse>()
                {
                    new SeatResponse()
                    {
                        Id = y.Seat.Id,
                        Scheduler = y.Seat.Scheduler,
                        Status = y.Seat.Status,
                        RoomSeat = y.Seat.RoomSeat,
                        SchedulerId = y.Seat.SchedulerId,
                        RoomsSeatId = y.Seat.RoomSeatId
                    }
                }
            });
        }
        
        var response = await query.PaginateAsync<BookingEntity,BookingResponse>(request, cancellationToken);
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
        return await _bookingDetailEntities.AsNoTracking().ProjectTo<BookingResponse>(_mapper.ConfigurationProvider).Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
    }
}