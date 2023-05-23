using System.Diagnostics.CodeAnalysis;
using Application.DataTransferObjects.Theater.Requests;
using MediatR;

namespace Application.Commands.Theater;
[ExcludeFromCodeCoverage]
public class UpdateTheaterCommand : UpdateTheaterRequest, IRequest<int>
{
    public UpdateTheaterCommand(Domain.Entities.Theater entity)
    {
        Entity = entity;
    }

    public Domain.Entities.Theater Entity { get; set; }
}
