using System.Diagnostics.CodeAnalysis;
using Application.DataTransferObjects.News.Requests;
using MediatR;

namespace Application.Commands.News;
[ExcludeFromCodeCoverage]
public class UpdateNewsCommand : UpdateNewsRequest, IRequest<int>
{
    public UpdateNewsCommand(Domain.Entities.News entity)
    {
        Entity = entity;
    }

    public Domain.Entities.News Entity { get; set; }
}
