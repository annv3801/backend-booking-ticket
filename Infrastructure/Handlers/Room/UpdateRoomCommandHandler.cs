using Application.Commands.Room;
using Application.Handlers.Room;
using Application.Repositories.Room;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.Room;

public class UpdateRoomCommandHandler : IUpdateRoomCommandHandler
{
    private readonly IRoomRepository _roomRepository;
    private readonly ApplicationDbContext _applicationDbContext;
    
    public UpdateRoomCommandHandler(ApplicationDbContext applicationDbContext, IRoomRepository roomRepository)
    {
        _applicationDbContext = applicationDbContext;
        _roomRepository = roomRepository;
    }

    public async Task<int> Handle(UpdateRoomCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _roomRepository.Update(command.Entity);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}