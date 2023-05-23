using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DataTransferObjects.Booking.Requests;
using Application.Logging.ActionLog.Services;
using Application.Repositories.Category;
using Application.Repositories.Seat;
using Application.Services.Booking;
using AutoMapper;
using Infrastructure.Databases;
using MediatR;

namespace Infrastructure.Services;
public class BookingManagementService : IBookingManagementService
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IEmailService _emailService;
    private readonly ISeatRepository _seatRepository;
    private readonly IVnPayService _vnPayService;

    public BookingManagementService(IActionLogService actionLogService, IStringLocalizationService localizationService, IJsonSerializerService jsonSerializerService, IMapper mapper, IPaginationService paginationService, ApplicationDbContext applicationDbContext, ICategoryRepository categoryRepository, IMediator mediator, IEmailService emailService, ISeatRepository seatRepository, IVnPayService vnPayService)
    {
        _applicationDbContext = applicationDbContext;
        _emailService = emailService;
        _seatRepository = seatRepository;
        _vnPayService = vnPayService;
    }
    
    public async Task<Result<BookingResult>> BookingAsync(BookingRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var seatNames = new List<string>();
            foreach (var seatId in request.SeatId)
            {
                var seat = await _seatRepository.GetSeatByIdAsync(seatId, cancellationToken);
                seatNames.Add(seat.Name);
            }
            
            var filmSchedule = _applicationDbContext.FilmSchedules.Where(x => x.Id == request.ScheduleId);
            var room = _applicationDbContext.Rooms;
            var p1 = filmSchedule.Join(room, x => x.RoomId, y => y.Id, (x, y) => new
            {
                x, y
            });
            var film = _applicationDbContext.Films;
            var p2 = p1.Join(film, x => x.x.FilmId, y => y.Id, (x, y) => new
            {
                x, y
            });
            var theater = _applicationDbContext.Theaters;
            var p3 = p2.Join(theater, x => x.x.y.TheaterId, y => y.Id, (x, y) => new
            {
                x, y   
            }).FirstOrDefault();
            var tableRows = "";
            for (int i = 0; i < seatNames.Count; i++)
            {
                var stt = (i + 1).ToString();
                var tenGhe = seatNames[i];
                var tongTien = request.TotalBeforeDiscount;

                var tableRow = $"<tr><td>{stt}</td><td>{p3.x.y.Name}</td><td>{p3.y.Name}</td><td>{p3.x.x.y.Name}</td><td>{p3.x.x.x.StartTime}</td><td style='font-weight:bold'>{tenGhe}</td><td>{(tongTien / seatNames.Count):N0}đ</td></tr>";
                tableRows += tableRow;
            }

            var body = $@"
            <html>
            <head>
                <style>
                    table {{
                        border-collapse: collapse;
                        width: 100%;
                        border: 1px solid #ddd;
                    }}
                    
                    td, th {{
                        border: 1px solid #ddd;
                        padding: 8px;
                    }}
                    
                    th {{
                        background-color: #f2f2f2;
                    }}
                </style>
            </head>
            <body>
                <table>
                    <tr>
                        <th>STT</th>
                        <th>Tên Phim</th>
                        <th>Rạp Chiếu</th>
                        <th>Phòng Chiếu</th>
                        <th>Thời gian bắt đầu</th>
                        <th>Tên ghế</th>
                        <th>Giảm giá</th>
                        <th>Tổng tiền</th>
                    </tr>
                    {tableRows}
                    <tr>
                        <td></td>
                        <td style='font-weight:bold'>Giảm giá</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td style='font-weight:bold'>{request.Discount:N0}đ</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style='font-weight:bold'>Tổng tiền</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td style='font-weight:bold'>{request.Total:N0}đ</td>
                    </tr>
                </table>
            </body>
            </html>";
            _emailService.SendEmail("nva030801@gmail.com", "Test", body);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (result > 0)
            {
                return Result<BookingResult>.Succeed();
            }
            return Result<BookingResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
