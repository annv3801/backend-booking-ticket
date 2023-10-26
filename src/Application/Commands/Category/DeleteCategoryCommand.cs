using MediatR;

namespace Application.Commands.Category;

public class DeleteCategoryCommand : IRequest<int>
{
    public required long Id { get; set; }
}