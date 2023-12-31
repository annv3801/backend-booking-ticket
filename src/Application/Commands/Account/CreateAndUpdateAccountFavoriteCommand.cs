using Domain.Entities;
using MediatR;

namespace Application.Commands.Account;

public class CreateAndUpdateAccountFavoriteCommand: IRequest<int>
{
    public long AccountId { get; set; }
    public long? FilmId { get; set; }
    public long? TheaterId { get; set; }
}
