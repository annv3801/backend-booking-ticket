using System.Diagnostics.CodeAnalysis;
using Application.DTO.Role.Responses;
using Application.Handlers.Role;
using Application.Queries.Role;
using Application.Services.Role;
using AutoMapper;
using Domain.Common.Interface;
using Nobi.Core.Responses;

namespace Infrastructure.Handlers.Role
{
    [ExcludeFromCodeCoverage]
    public class ViewRoleHandler : IViewRoleHandler
    {
        private readonly IRoleManagementService _roleManagementService;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;

        public ViewRoleHandler(IRoleManagementService roleManagementService, ILoggerService loggerService, IMapper mapper)
        {
            _roleManagementService = roleManagementService;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        public async Task<RequestResult<ViewRoleResponse>> Handle(ViewRoleQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roleManagementService.ViewRoleAsync(query.Id, cancellationToken);
                if (result.Success)
                    return RequestResult<ViewRoleResponse>.Succeed(data: result.Data);
                return RequestResult<ViewRoleResponse>.Fail(result.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RequestResult<ViewRoleResponse>.Fail("Fail");
            }
        }
    }
}