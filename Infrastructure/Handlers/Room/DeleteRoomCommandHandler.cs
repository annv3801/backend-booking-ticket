using Application.Commands.Room;
using Application.Handlers.Room;
using Application.Repositories.Room;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.Room;

public class DeleteRoomCommandHandler : IDeleteRoomCommandHandler
{
    private readonly IRoomRepository _roomRepository;
    private readonly ApplicationDbContext _applicationDbContext;

    public DeleteRoomCommandHandler(IRoomRepository roomRepository, ApplicationDbContext applicationDbContext)
    {
        _roomRepository = roomRepository;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<int> Handle(DeleteRoomCommand command, CancellationToken cancellationToken)
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