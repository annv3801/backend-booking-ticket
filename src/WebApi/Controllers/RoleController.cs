using Application.Commands.Role;
using Application.Common;
using Application.DataTransferObjects.Role.Requests;
using Application.Queries.Role;
using AutoMapper;
using Domain.Common.Interface;
using Infrastructure.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    /// <inheritdoc />
    [ApiController]
    [Route("Role")]

    public class RoleController : ControllerBase
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
        public RoleController(IMediator mediator, IMapper mapper, ILoggerService loggerService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        /// <summary>
        /// Create Role
        /// </summary>
        /// <param name="createRoleRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create-Role")]
        [Produces(Constants.MimeTypes.Application.Json)]
        public async Task<IActionResult> CreateRoleAsync(CreateRoleRequest createRoleRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var command = _mapper.Map<CreateRoleCommand>(createRoleRequest);
                var result = await _mediator.Send(command, cancellationToken);
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
        /// Update Role
        /// </summary>
        /// <param name="roleId">ID to update</param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update-Role")]
        [Produces(Constants.MimeTypes.Application.Json)]
        public async Task<IActionResult> UpdateRoleAsync(long roleId, UpdateRoleRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var command = _mapper.Map<UpdateRoleCommand>(request);
                command.Id = roleId;
                var result = await _mediator.Send(command, cancellationToken);
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
        /// View Role
        /// </summary>
        /// <param name="roleId">ID to view</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("View-Role")]
        [Produces(Constants.MimeTypes.Application.Json)]
        public async Task<IActionResult> ViewRoleAsync(long roleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await _mediator.Send(new ViewRoleQuery() {Id = roleId}, cancellationToken);
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
        /// View list of Roles
        /// </summary>
        /// <remarks>
        ///
        /// Sortable:
        ///
        ///     Name, Status
        /// </remarks>
        /// <param name="viewListRolesRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("View-List-Roles")]
        [Produces(Constants.MimeTypes.Application.Json)]
        public async Task<IActionResult> ViewListRolesAsync([FromQuery] ViewListRolesRequest viewListRolesRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await _mediator.Send(_mapper.Map<ViewListRolesQuery>(viewListRolesRequest), cancellationToken);
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
        /// Delete Role
        /// </summary>
        /// <param name="roleId">ID to delete</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete-Role")]
        [Produces(Constants.MimeTypes.Application.Json)]
        public async Task<IActionResult> DeleteRoleAsync(long roleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await _mediator.Send(new DeleteRoleCommand() {Id = roleId}, cancellationToken);
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
        /// Activate Role
        /// </summary>
        /// <param name="roleId">ID to activate</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Activate-Role")]
        [Produces(Constants.MimeTypes.Application.Json)]
        public async Task<IActionResult> ActivateRoleAsync(long roleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await _mediator.Send(new ActivateRoleCommand() {Id = roleId}, cancellationToken);
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
        /// Deactivate Role
        /// </summary>
        /// <param name="roleId">ID to deactivate</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Deactivate-Role")]
        [Produces(Constants.MimeTypes.Application.Json)]
        public async Task<IActionResult> DeactivateRoleAsync(long roleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await _mediator.Send(new DeactivateRoleCommand() {Id = roleId}, cancellationToken);
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