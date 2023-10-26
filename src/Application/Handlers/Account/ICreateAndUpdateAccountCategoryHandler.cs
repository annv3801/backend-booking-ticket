using Application.Commands.Account;
using Application.Common;
using Application.Common.Models;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Handlers.Account;
public interface ICreateAndUpdateAccountCategoryHandler : IRequestHandler<CreateAndUpdateAccountCategoryCommand, int>
{
}
