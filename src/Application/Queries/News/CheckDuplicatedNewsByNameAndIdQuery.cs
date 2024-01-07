using MediatR;

namespace Application.Queries.News;

public class CheckDuplicatedNewsByNameAndIdQuery : IRequest<bool>
{
    public string Title { get; set; }
    public long Id { get; set; }
}