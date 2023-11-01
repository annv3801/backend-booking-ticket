using Application.DataTransferObjects.Theater.Responses;
using MediatR;

namespace Application.Queries.Theater;

public class GetTheaterByIdQuery : IRequest<TheaterResponse?>
{
    public long Id { get; set; }
}