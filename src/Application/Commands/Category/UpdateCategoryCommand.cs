using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DataTransferObjects.Category.Requests;
using MediatR;

namespace Application.Commands.Category;
[ExcludeFromCodeCoverage]
public class UpdateCategoryCommand : UpdateCategoryRequest, IRequest<int>
{
    public UpdateCategoryCommand(Domain.Entities.Category entity)
    {
        Entity = entity;
    }

    public Domain.Entities.Category Entity { get; set; }
}
