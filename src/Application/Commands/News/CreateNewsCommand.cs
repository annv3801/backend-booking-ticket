using Domain.Entities;
using MediatR;

namespace Application.Commands.News;

public class CreateNewsCommand : IRequest<int>
{
    public NewsEntity Entity { get; set; }
}