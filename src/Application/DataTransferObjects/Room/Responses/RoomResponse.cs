using Domain.Entities;

namespace Application.DataTransferObjects.Room.Responses;

public class RoomResponse 
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public long TheaterId { get; set; }
    public TheaterEntity Theater { get; set; }
}
