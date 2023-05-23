using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Room.Responses;
using MediatR;

namespace Application.Queries.Room;
[ExcludeFromCodeCoverage]
public class ViewRoomByIdQuery : IRequest<Result<ViewRoomResponse>>
{
    public ViewRoomByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
