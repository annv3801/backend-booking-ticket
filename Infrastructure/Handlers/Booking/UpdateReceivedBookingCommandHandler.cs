using Application.Commands.Booking;
using Application.Commands.Category;
using Application.Handlers.Booking;
using Application.Handlers.Category;
using Application.Repositories.Booking;
using Application.Repositories.Category;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.Booking;

public class UpdateReceivedBookingCommandHandler : IUpdateReceivedBookingCommandHandler
{
    private readonly IBookingRepository _bookingRepository;
    private readonly ApplicationDbContext _applicationDbContext;
    
    public UpdateReceivedBookingCommandHandler(ApplicationDbContext applicationDbContext, IBookingRepository bookingRepository)
    {
        _applicationDbContext = applicationDbContext;
        _bookingRepository = bookingRepository;
    }


    public async Task<int> Handle(UpdateReceivedBookingCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _bookingRepository.Update(request.Entity);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}