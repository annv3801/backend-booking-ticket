// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Application.DataTransferObjects.Room.Requests;

public class CreateRoomRequest
{
    public string Name { get; set; }
    public long TheaterId { get; set; }
}