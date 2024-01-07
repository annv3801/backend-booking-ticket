using Domain.Entities;
using MediatR;

namespace Application.Commands.News;

public class UpdateNewsCommand : IRequest<int>
{
    public NewsEntity Request { get; set; }
}