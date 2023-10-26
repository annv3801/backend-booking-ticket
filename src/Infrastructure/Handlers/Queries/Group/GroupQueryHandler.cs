using Application.DataTransferObjects.Group.Responses;
using Application.Queries.Group;
using Application.Repositories.Group;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Infrastructure.Handlers.Queries.Group;

public class GroupQueryHandler :
    IRequestHandler<GetGroupByIdQuery, GroupResponse?>, 
    IRequestHandler<GetListCategoriesQuery, OffsetPaginationResponse<GroupResponse>>,
    IRequestHandler<CheckDuplicatedGroupByNameAndIdQuery, bool>,
    IRequestHandler<CheckDuplicatedGroupByNameQuery, bool>
{
    private readonly IGroupRepository _groupRepository;

    public GroupQueryHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<GroupResponse?> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
    {
        return await _groupRepository.GetGroupByIdAsync(request.Id, cancellationToken);
    }
    
    public async Task<OffsetPaginationResponse<GroupResponse>> Handle(GetListCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _groupRepository.GetListGroupsAsync(request.OffsetPaginationRequest, cancellationToken);
    }
    
    public async Task<bool> Handle(CheckDuplicatedGroupByNameAndIdQuery request, CancellationToken cancellationToken)
    {
        return await _groupRepository.IsDuplicatedGroupByNameAndIdAsync(request.Title, request.Id, cancellationToken);
    }
    
    public async Task<bool> Handle(CheckDuplicatedGroupByNameQuery request, CancellationToken cancellationToken)
    {
        return await _groupRepository.IsDuplicatedGroupByNameAsync(request.Title, cancellationToken);
    }
}