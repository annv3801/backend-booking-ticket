using System.Diagnostics.CodeAnalysis;
using Application.Commands.Permission;
using Application.Common;
using Application.Handlers.Permission;
using Application.Services.Permission;
using AutoMapper;
using Domain.Common.Interface;
using Nobi.Core.Responses;

namespace Infrastructure.Handlers.Permission
{
    [ExcludeFromCodeCoverage]
    public class UpdatePermissionHandler : IUpdatePermissionHandler
    {
        private readonly IPermissionManagementService _permissionManagementService;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;

        public UpdatePermissionHandler(IPermissionManagementService permissionManagementService, ILoggerService loggerService, IMapper mapper)
        {
            _permissionManagementService = permissionManagementService;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        public async Task<RequestResult<PermissionResult>> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var perm = _mapper.Map<Domain.Entities.Identity.Permission>(request);
                return await _permissionManagementService.UpdatePermissionAsync(perm, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RequestResult<PermissionResult>.Fail("Fail");
            }
        }
    }
}