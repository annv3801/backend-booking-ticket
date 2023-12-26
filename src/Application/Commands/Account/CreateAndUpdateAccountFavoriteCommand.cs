using Domain.Entities;
using MediatR;

namespace Application.Commands.Account;

public class CreateAndUpdateAccountFavoriteCommand: IRequest<int>
{
    public long AccountId { get; set; }
    
}
