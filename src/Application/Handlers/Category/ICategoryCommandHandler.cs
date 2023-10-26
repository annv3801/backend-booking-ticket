using Application.Commands.Category;
using MediatR;

namespace Application.Handlers.Category;

public interface ICreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
}

public interface IUpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, int>
{
}

public interface IDeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, int>
{
}
