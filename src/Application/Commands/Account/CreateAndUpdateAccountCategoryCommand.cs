using Domain.Entities;
using MediatR;

namespace Application.Commands.Account;

public class CreateAndUpdateAccountCategoryCommand: IRequest<int>
{
    public required ICollection<long> CategoryIds { get; set; }
    public required long AccountId { get; set; }
}
