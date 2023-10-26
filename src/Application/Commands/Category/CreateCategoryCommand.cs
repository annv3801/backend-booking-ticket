using Domain.Entities;
using MediatR;

namespace Application.Commands.Category;

public class CreateCategoryCommand : IRequest<int>
{
    public required CategoryEntity Entity { get; set; }
}