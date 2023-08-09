using MediatR;

namespace Application.Commands.Theater;

public class DeleteTheaterCommand : IRequest<int>
{
    public DeleteTheaterCommand(Domain.Entities.Theater entity)
    {
        Entity = entity;
    }

    public Domain.Entities.Theater Entity { get; set; }
}