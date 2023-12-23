using Application.Commands.Slide;
using MediatR;

namespace Application.Handlers.Slide;

public interface ICreateSlideCommandHandler : IRequestHandler<CreateSlideCommand, int>
{
}

public interface IUpdateSlideCommandHandler : IRequestHandler<UpdateSlideCommand, int>
{
}

public interface IDeleteSlideCommandHandler : IRequestHandler<DeleteSlideCommand, int>
{
}
