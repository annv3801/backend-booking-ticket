using Application.DataTransferObjects.News.Requests;
using MediatR;

namespace Application.Commands.News;

public class CreateNewsCommand : CreateNewsRequest, IRequest<int>
{
    public CreateNewsCommand(Domain.Entities.News entity)
    {
        Entity = entity;
    }

    public Domain.Entities.News Entity { get; set; }
}