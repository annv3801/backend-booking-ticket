using Application.Commands.Category;
using MediatR;

namespace Application.Handlers.Category;

public interface IUpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, int>
{
}