using System.Diagnostics.CodeAnalysis;
using Application.DataTransferObjects.Permission.Responses;
using Application.Handlers.Permission;
using Application.Queries.Permission;
using Application.Services.Permission;
using AutoMapper;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Nobi.Core.Responses;

namespace Infrastructure.Handlers.Permission
{
    [ExcludeFromCodeCoverage]
    public class ViewListPermissionsHandler : IViewListPermissionsHandler
    {
        private readonly IPermissionManagementService _permissionManagementService;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;

        public ViewListPermissionsHandler(IPermissionManagementService permissionManagementService, ILoggerService loggerService, IMapper mapper)
        {
            _permissionManagementService = permissionManagementService;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        public async Task<RequestResult<OffsetPaginationResponse<ViewPermissionResponse>>> Handle(ViewListPermissionsQuery query, CancellationToken cancellationToken)
        {
            try
            {
                return await _permissionManagementService.ViewListPermissionsAsync(query, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RequestResult<OffsetPaginationResponse<ViewPermissionResponse>>.Fail("Fail");
            }
        }
    }
}