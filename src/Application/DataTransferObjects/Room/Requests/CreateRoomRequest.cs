namespace Application.DataTransferObjects.Room.Requests;

public class CreateRoomRequest
{
    public string Name { get; set; }
    public int Status { get; set; }
    public Guid TheaterId { get; set; }
}