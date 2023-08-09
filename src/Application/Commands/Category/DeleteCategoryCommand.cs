using Application.Common;
using Application.Common.Models;
using MediatR;

namespace Application.Commands.Category;

public class DeleteCategoryCommand : IRequest<int>
{
    public DeleteCategoryCommand(Domain.Entities.Category entity)
    {
        Entity = entity;
    }

    public Domain.Entities.Category Entity { get; set; }
}