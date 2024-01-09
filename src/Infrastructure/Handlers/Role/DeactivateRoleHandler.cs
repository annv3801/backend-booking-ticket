using System.Diagnostics.CodeAnalysis;
using Application.Commands.Role;
using Application.Common;
using Application.Handlers.Role;
using Application.Services.Role;
using AutoMapper;
using Domain.Common.Interface;
using Nobi.Core.Responses;

namespace Infrastructure.Handlers.Role
{
    [ExcludeFromCodeCoverage]
    public class DeactivateRoleHandler : IDeactivateRoleHandler
    {
        private readonly IRoleManagementService _roleManagementService;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;

        public DeactivateRoleHandler(IRoleManagementService roleManagementService, ILoggerService loggerService, IMapper mapper)
        {
            _roleManagementService = roleManagementService;
            _loggerService = loggerService;
            _mapper = mapper;
        }


        public async Task<RequestResult<RoleResult>> Handle(DeactivateRoleCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roleManagementService.DeactivateRoleAsync(command.Id, cancellationToken);
                if (result.Success)
                    return RequestResult<RoleResult>.Succeed(data: result.Data);
                return RequestResult<RoleResult>.Fail(result.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RequestResult<RoleResult>.Fail("Fail");
            }
        }
    }
}