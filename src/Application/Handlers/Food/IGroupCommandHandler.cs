using Application.Commands.Food;
using MediatR;

namespace Application.Handlers.Food;

public interface ICreateFoodCommandHandler : IRequestHandler<CreateFoodCommand, int>
{
}

public interface IUpdateFoodCommandHandler : IRequestHandler<UpdateFoodCommand, int>
{
}

public interface IDeleteFoodCommandHandler : IRequestHandler<DeleteFoodCommand, int>
{
}
