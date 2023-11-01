using Application.Commands.Film;
using MediatR;

namespace Application.Handlers.Film;

public interface ICreateFilmCommandHandler : IRequestHandler<CreateFilmCommand, int>
{
}

public interface IUpdateFilmCommandHandler : IRequestHandler<UpdateFilmCommand, int>
{
}

public interface IDeleteFilmCommandHandler : IRequestHandler<DeleteFilmCommand, int>
{
}

public interface ICreateFeedbackFilmCommandHandler : IRequestHandler<CreateFeedbackFilmCommand, int>
{
}
