﻿using Domain.Entities;
using MediatR;

namespace Application.Commands.Account;

public class CreateAndUpdateAccountCategoryCommand: IRequest<int>
{
    public ICollection<long> CategoryIds { get; set; }
    public long AccountId { get; set; }
}
