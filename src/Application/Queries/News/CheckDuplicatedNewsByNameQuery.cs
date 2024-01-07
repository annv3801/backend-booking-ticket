using MediatR;

namespace Application.Queries.News;

public class CheckDuplicatedNewsByNameQuery :  IRequest<bool>
{
    public string Title { get; set; }
}