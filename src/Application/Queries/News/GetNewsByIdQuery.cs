using Application.DataTransferObjects.Food.Responses;
using Application.DataTransferObjects.News.Responses;
using MediatR;

namespace Application.Queries.News;

public class GetNewsByIdQuery : IRequest<NewsResponse?>
{
    public long Id { get; set; }
}