using Domain.Entities;
using MediatR;

namespace Application.Commands.Category;

public class CreateCategoryCommand : IRequest<int>
{
    public CategoryEntity Entity { get; set; }
}