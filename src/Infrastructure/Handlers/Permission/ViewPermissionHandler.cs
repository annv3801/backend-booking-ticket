using System.Diagnostics.CodeAnalysis;
using Application.DataTransferObjects.Permission.Responses;
using Application.Handlers.Permission;
using Application.Queries.Permission;
using Application.Services.Permission;
using Domain.Common.Interface;
using Nobi.Core.Responses;

namespace Infrastructure.Handlers.Permission
{
    [ExcludeFromCodeCoverage]
    public class ViewPermissionHandler : IViewPermissionHandler
    {
        private readonly ILoggerService _loggerService;
        private readonly IPermissionManagementService _permissionManagementService;

        public ViewPermissionHandler(ILoggerService loggerService, IPermissionManagementService permissionManagementService)
        {
            _loggerService = loggerService;
            _permissionManagementService = permissionManagementService;
        }

        public async Task<RequestResult<ViewPermissionResponse>> Handle(ViewPermissionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _permissionManagementService.ViewPermissionAsync(request.PermissionId, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RequestResult<ViewPermissionResponse>.Fail("Fail");
            }
        }
    }
}