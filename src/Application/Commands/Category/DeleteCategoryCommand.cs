using MediatR;

namespace Application.Commands.Category;

public class DeleteCategoryCommand : IRequest<int>
{
    public long Id { get; set; }
}