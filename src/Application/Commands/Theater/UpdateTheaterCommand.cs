using Domain.Entities;
using MediatR;

namespace Application.Commands.Theater;

public class UpdateTheaterCommand : IRequest<int>
{
    public TheaterEntity Request { get; set; }
}