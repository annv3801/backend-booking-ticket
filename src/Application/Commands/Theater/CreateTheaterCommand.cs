using Application.DataTransferObjects.Theater.Requests;
using MediatR;

namespace Application.Commands.Theater;

public class CreateTheaterCommand : CreateTheaterRequest, IRequest<int>
{
    public CreateTheaterCommand(Domain.Entities.Theater entity)
    {
        Entity = entity;
    }

    public Domain.Entities.Theater Entity { get; set; }
}