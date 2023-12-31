using Domain.Common.Entities;
using Domain.Entities.Identity;

namespace Domain.Entities;

public class AccountFavoritesEntity : Entity
{
    public long Id { get; set; }
    public long AccountId { get; set; }
    public Account Account { get; set; }
    public long? TheaterId { get; set; }
    public TheaterEntity Theater { get; set; }
    public long? FilmId { get; set; }
    public FilmEntity Film { get; set; }
}