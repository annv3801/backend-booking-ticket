using Domain.Entities;
using MediatR;

namespace Application.Commands.Category;

public class UpdateCategoryCommand : IRequest<int>
{
    public CategoryEntity Request { get; set; }
}