using Application.Commands.Permission;
using Application.Common;
using Application.DataTransferObjects.Permission.Requests;
using Application.Queries.Permission;
using AutoMapper;
using Domain.Common.Interface;
using Infrastructure.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    /// <inheritdoc />
    [ApiController]
    [Route("Permission")]
    public class PermissionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="mapper"></param>
        /// <param name="loggerService"></param>
        public PermissionController(IMediator mediator, IMapper mapper, ILoggerService loggerService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        /// <summary>
        /// To update Permission
        /// </summary>
        /// <param name="permId">Permission Id</param>
        /// <param name="updatePermissionRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update-Permission")]
        [Produces(Constants.MimeTypes.Application.Json)]
        public async Task<IActionResult> UpdatePermissionAsync(long permId, UpdatePermissionRequest updatePermissionRequest, CancellationToken cancellationToken)
        {
            try
            {
                var updatePermissionCommand = new UpdatePermissionCommand()
                {
                    Id = permId,
                    Description = updatePermissionRequest.Description,
                    Name = updatePermissionRequest.Name
                };
                var result = await _mediator.Send(updatePermissionCommand, cancellationToken);
                if (result.Success)
                    return Ok(new SuccessResponse());
                return Accepted(new FailureResponse(result.Errors));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// To view Permission
        /// </summary>
        /// <param name="permId">Permission Id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("View-Permission")]
        [Produces(Constants.MimeTypes.Application.Json)]
        public async Task<IActionResult> ViewPermissionAsync(long permId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(new ViewPermissionQuery() {PermissionId = permId}, cancellationToken);
                if (result.Success)
                    return Ok(new SuccessResponse(data: result.Data));
                return Accepted(new FailureResponse(result.Errors));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// To view list Permissions
        /// </summary>
        /// <remarks>
        ///
        /// Sortable:
        ///
        ///     Name, Code
        /// </remarks>
        /// <param name="viewListPermissionsRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("View-List-Permissions")]
        [Produces(Constants.MimeTypes.Application.Json)]
        public async Task<IActionResult> ViewListPermissionsAsync([FromQuery] ViewListPermissionsRequest viewListPermissionsRequest, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(_mapper.Map<ViewListPermissionsQuery>(viewListPermissionsRequest), cancellationToken);
                if (result.Success)
                    return Ok(new SuccessResponse(data: result.Data));
                return Accepted(new FailureResponse(result.Errors));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}