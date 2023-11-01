using Domain.Entities;
using MediatR;

namespace Application.Commands.Theater;

public class CreateTheaterCommand : IRequest<int>
{
    public required TheaterEntity Entity { get; set; }
}