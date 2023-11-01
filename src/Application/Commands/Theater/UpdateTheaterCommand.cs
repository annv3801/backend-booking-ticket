using Domain.Entities;
using MediatR;

namespace Application.Commands.Theater;

public class UpdateTheaterCommand : IRequest<int>
{
    public required TheaterEntity Request { get; set; }
}