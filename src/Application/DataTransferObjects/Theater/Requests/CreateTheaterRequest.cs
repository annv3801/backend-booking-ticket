namespace Application.DataTransferObjects.Theater.Requests;

public class CreateTheaterRequest
{
    public string Name { get; set; }
    public string Address { get; set; }
    public int Status { get; set; }
}