using Application.DataTransferObjects.Dashboard;
using Application.Repositories.Booking;
using Application.Repositories.Film;
using Domain.Common.Interface;
using Microsoft.AspNetCore.Mvc;
using Nobi.Core.Responses;

namespace WebApi.Controllers;

/// <summary>
/// Provides API endpoints for managing Categories. Supports creating, updating, 
/// deleting and fetching Dashboard, and listing Categories with pagination. 
/// Exceptions are logged using an injected ILoggerService instance.
/// </summary>
[ApiController]
[Route("Dashboard")]
public class DashboardController : ControllerBase
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IBookingDetailRepository _bookingDetailRepository;
    private readonly IFilmRepository _filmRepository;
    private readonly ILoggerService _loggerService;

    public DashboardController(ILoggerService loggerService, IBookingRepository bookingRepository, IBookingDetailRepository bookingDetailRepository, IFilmRepository filmRepository)
    {
        _loggerService = loggerService;
        _bookingRepository = bookingRepository;
        _bookingDetailRepository = bookingDetailRepository;
        _filmRepository = filmRepository;
    }

    /// <summary>
    /// Create new Dashboard
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("Dashboard")]
    public async Task<RequestResult<DashboardResponse>?> DashboardAsync(CancellationToken cancellationToken)
    {
        try
        {
            var countFilm = _filmRepository.Entity.Distinct().Count();
            var countCustomer = _bookingRepository.Entity
                .Select(b => b.AccountId)
                .Distinct()
                .Count();

            var countSeatSell = _bookingDetailRepository.Entity
                .Select(bd => bd.SeatId)
                .Distinct()
                .Count();

            var totalPrice = _bookingRepository.Entity
                .Sum(b => b.Total);

            return RequestResult<DashboardResponse>.Succeed(data: new DashboardResponse()
            {
                CountCustomer = countCustomer,
                TotalPrice = totalPrice,
                CountSeatSell = countSeatSell,
                CountFilm = countFilm
            });
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(DashboardAsync));
            throw;
        }
    }

    
}