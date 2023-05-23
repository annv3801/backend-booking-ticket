using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Seat.Responses;
using MediatR;

namespace Application.Queries.Seat;
[ExcludeFromCodeCoverage]
public class ViewSeatByIdQuery : IRequest<Result<ViewSeatResponse>>
{
    public ViewSeatByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
