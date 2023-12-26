namespace Application.DataTransferObjects.Seat.Requests;

public class CreateListSeatRequest
{
    public List<CreateSeatRequest> CreateSeatRequests { get; set; }
}