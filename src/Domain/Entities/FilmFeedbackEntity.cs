using Domain.Common.Entities;
using Domain.Entities.Identity;

namespace Domain.Entities;

public class FilmFeedbackEntity : Entity<long>
{
    public long Id { get; set; }
    public long FilmId { get; set; }
    public FilmEntity Film { get; set; }
    public long AccountId { get; set; }
    public Account Account { get; set; }
    public int Rating { get; set; }
}