using MediatR;

namespace Application.Commands.News;

public class DeleteNewsCommand : IRequest<int>
{
    public DeleteNewsCommand(Domain.Entities.News entity)
    {
        Entity = entity;
    }

    public Domain.Entities.News Entity { get; set; }
}