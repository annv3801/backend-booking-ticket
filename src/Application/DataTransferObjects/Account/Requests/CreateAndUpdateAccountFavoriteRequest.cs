namespace Application.DataTransferObjects.Account.Requests;

public class CreateAndUpdateAccountFavoriteRequest
{
    public long? FilmId { get; set; }
    public long? TheaterId { get; set; }
}