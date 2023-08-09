using Application.Common;
using Application.Common.Models;
using Application.DataTransferObjects.Category.Requests;
using MediatR;

namespace Application.Commands.Category;

public class CreateCategoryCommand : CreateCategoryRequest, IRequest<int>
{
    public CreateCategoryCommand(Domain.Entities.Category entity)
    {
        Entity = entity;
    }

    public Domain.Entities.Category Entity { get; set; }
}