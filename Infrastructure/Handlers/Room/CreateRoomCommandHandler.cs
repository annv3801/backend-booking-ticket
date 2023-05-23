using Application.Commands.News;
using Application.Commands.Room;
using Application.Handlers.News;
using Application.Handlers.Room;
using Application.Repositories.News;
using Application.Repositories.Room;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.Room;

public class CreateRoomCommandHandler : ICreateRoomCommandHandler
{
    private readonly IRoomRepository _roomRepository;
    private readonly ApplicationDbContext _applicationDbContext;

    public CreateRoomCommandHandler(IRoomRepository roomRepository, ApplicationDbContext applicationDbContext)
    {
        _roomRepository = roomRepository;
        _applicationDbContext = applicationDbContext;
    }


    public async Task<int> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _roomRepository.AddAsync(command.Entity, cancellationToken);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}