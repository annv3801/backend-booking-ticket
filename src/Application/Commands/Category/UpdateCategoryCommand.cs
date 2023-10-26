using Domain.Entities;
using MediatR;

namespace Application.Commands.Category;

public class UpdateCategoryCommand : IRequest<int>
{
    public required CategoryEntity Request { get; set; }
}