using Application.Common;
using Application.Common.Interfaces;
using Application.DataTransferObjects.Permission.Responses;
using Application.Queries.Permission;
using Application.Repositories.Permission;
using Application.Services.Permission;
using AutoMapper;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Domain.Extensions;
using Nobi.Core.Responses;

namespace Infrastructure.Services
{
    public class PermissionManagementService : IPermissionManagementService
    {
        private readonly IStringLocalizationService _localizationService;
        private readonly ILoggerService _loggerService;
        private readonly IJsonSerializerService _jsonSerializerService;
        private readonly IMapper _mapper;
        private readonly IPermissionRepository _permissionRepository;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="localizationService"></param>
        /// <param name="actionLogService"></param>
        /// <param name="loggerService"></param>
        /// <param name="jsonSerializerService"></param>
        /// <param name="mapper"></param>
        public PermissionManagementService(IStringLocalizationService localizationService, ILoggerService loggerService, IJsonSerializerService jsonSerializerService, IMapper mapper, IPermissionRepository permissionRepository)
        {
            _localizationService = localizationService;
            _loggerService = loggerService;
            _jsonSerializerService = jsonSerializerService;
            _mapper = mapper;
            _permissionRepository = permissionRepository;
        }

        public async Task<RequestResult<PermissionResult>> UpdatePermissionAsync(Domain.Entities.Identity.Permission permission, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                // Find object
                var perm = await _permissionRepository.FindPermissionById(permission.Id, cancellationToken);
                if (perm == null)
                    return RequestResult<PermissionResult>.Fail("Not Found Permission");
                // Copy data
                perm.Name = permission.Name;
                perm.Description = permission.Description;
                
                await _permissionRepository.UpdateAsync(perm, cancellationToken);
                var result = await _permissionRepository.SaveChangesAsync(cancellationToken);
                if (result > 0)
                {
                    return RequestResult<PermissionResult>.Succeed();
                }

                return RequestResult<PermissionResult>.Fail("Save fail");
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<RequestResult<ViewPermissionResponse>> ViewPermissionAsync(long permId, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                // Get permission
                var perm = await _permissionRepository.FindPermissionByIdWithAuditableEntity(permId, cancellationToken);
                if (perm == null)
                    return RequestResult<ViewPermissionResponse>.Fail("Not found permission");
                return RequestResult<ViewPermissionResponse>.Succeed(data: _mapper.Map<ViewPermissionResponse>(perm));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<RequestResult<OffsetPaginationResponse<ViewPermissionResponse>>> ViewListPermissionsAsync(ViewListPermissionsQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var queryable = await _permissionRepository.SearchPermissionByName(query, cancellationToken);
                var response = await queryable.PaginateAsync<Domain.Entities.Identity.Permission,ViewPermissionResponse>(query, cancellationToken);

                return RequestResult<OffsetPaginationResponse<ViewPermissionResponse>>.Succeed(data: _mapper.Map<OffsetPaginationResponse<ViewPermissionResponse>>(response));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}