using System.Diagnostics.CodeAnalysis;
using Application.DTO.Role.Responses;
using Application.Handlers.Role;
using Application.Queries.Role;
using Application.Services.Role;
using AutoMapper;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Nobi.Core.Responses;

namespace Infrastructure.Handlers.Role
{
    [ExcludeFromCodeCoverage]
    public class ViewListRolesHandler : IViewListRolesHandler
    {
        private readonly IRoleManagementService _roleManagementService;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;

        public ViewListRolesHandler(IRoleManagementService roleManagementService, ILoggerService loggerService, IMapper mapper)
        {
            _roleManagementService = roleManagementService;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        public async Task<RequestResult<OffsetPaginationResponse<ViewRoleResponse>>> Handle(ViewListRolesQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roleManagementService.ViewListRolesAsync(query, cancellationToken);
                if (result.Success)
                    return RequestResult<OffsetPaginationResponse<ViewRoleResponse>>.Succeed(data: result.Data);
                return RequestResult<OffsetPaginationResponse<ViewRoleResponse>>.Fail(result.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RequestResult<OffsetPaginationResponse<ViewRoleResponse>>.Fail("Fail");
            }
        }
    }
}