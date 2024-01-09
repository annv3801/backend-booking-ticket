using Application.Common;
using Application.DataTransferObjects.Permission.Requests;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Commands.Permission;

public class UpdatePermissionCommand : UpdatePermissionRequest, IRequest<RequestResult<PermissionResult>>
{
    public long Id { get; set; }
}