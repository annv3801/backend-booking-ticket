﻿using Application.Commands.Film;
using MediatR;

namespace Application.Handlers.Film;

public interface IUpdateFilmCommandHandler : IRequestHandler<UpdateFilmCommand, int>
{
}