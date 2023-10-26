﻿using Application.Commands.Group;
using Application.Common.Interfaces;
using Application.DataTransferObjects.Group.Requests;
using Application.DataTransferObjects.Group.Responses;
using Application.Interface;
using Application.Queries.Group;
using Application.Repositories.Group;
using Application.Services.Group;
using AutoMapper;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nobi.Core.Responses;

// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract


namespace Infrastructure.Services;

public class GroupManagementService : IGroupManagementService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ILoggerService _loggerService;
    private readonly IGroupRepository _groupRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentAccountService _currentAccountService;

    public GroupManagementService(IMapper mapper, IMediator mediator, ILoggerService loggerService, IGroupRepository groupRepository,
        IDateTimeService dateTimeService, ICurrentAccountService currentAccountService)
    {
        _mapper = mapper;
        _mediator = mediator;
        _loggerService = loggerService;
        _groupRepository = groupRepository;
        _dateTimeService = dateTimeService;
        _currentAccountService = currentAccountService;
    }

    public async Task<RequestResult<bool>> CreateGroupAsync(CreateGroupRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var parentPath = string.Empty;

            // Check duplicate Group name
            if (await _mediator.Send(new CheckDuplicatedGroupByNameQuery
                {
                    Title = request.Title,
                }, cancellationToken))
                return RequestResult<bool>.Fail("Item is duplicated");

            // Create Group 
            var GroupEntity = _mapper.Map<GroupEntity>(request);

            GroupEntity.CreatedBy = _currentAccountService.Id;
            GroupEntity.CreatedTime = _dateTimeService.NowUtc;

            var resultCreateGroup = await _mediator.Send(new CreateGroupCommand {Entity = GroupEntity}, cancellationToken);
            if (resultCreateGroup <= 0)
                return RequestResult<bool>.Fail("Save data failed");
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateGroupAsync));
            throw;
        }
    }

    public async Task<RequestResult<bool>> UpdateGroupAsync(UpdateGroupRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var parentPath = string.Empty;
            // Check duplicate Group name
            if (await _mediator.Send(new CheckDuplicatedGroupByNameAndIdQuery
                {
                    Title = request.Title,
                    Id = request.Id,
                }, cancellationToken))
                return RequestResult<bool>.Fail("Item is duplicated");


            var existedGroup = await _groupRepository.GetGroupEntityByIdAsync(request.Id, cancellationToken);
            if (existedGroup == null)
                return RequestResult<bool>.Fail("Group is not found");

            
            // Update value to existed Group
            existedGroup.Title = request.Title;

            var resultUpdateGroup = await _mediator.Send(new UpdateGroupCommand
            {
                Request = existedGroup,
            }, cancellationToken);
            if (resultUpdateGroup <= 0)
                return RequestResult<bool>.Fail("Save data failed");
           
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(UpdateGroupAsync));
            throw;
        }
    }

    public async Task<RequestResult<bool>> DeleteGroupAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            // Check for existence
            var GroupToDelete = await _groupRepository.GetGroupByIdAsync(id, cancellationToken);
            if (GroupToDelete == null)
                return RequestResult<bool>.Fail("Group is not found");


            var resultDeleteGroup = await _mediator.Send(new DeleteGroupCommand
            {
                Id = id,
            }, cancellationToken);
            if (resultDeleteGroup <= 0)
                return RequestResult<bool>.Fail("Save data failed");
           
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(DeleteGroupAsync));
            throw;
        }
    }

    public async Task<RequestResult<GroupResponse>> GetGroupAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var Group = await _mediator.Send(new GetGroupByIdQuery
            {
                Id = id,
            }, cancellationToken);
            if (Group == null)
                return RequestResult<GroupResponse>.Fail("Group is not found");

            return RequestResult<GroupResponse>.Succeed(null, _mapper.Map<GroupResponse>(Group));
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetGroupAsync));
            throw;
        }
    }

    public async Task<RequestResult<OffsetPaginationResponse<GroupResponse>>> GetListGroupsAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // check tenant valid
            var Group = await _mediator.Send(new GetListCategoriesQuery
            {
                OffsetPaginationRequest = request,
            }, cancellationToken);

            return RequestResult<OffsetPaginationResponse<GroupResponse>>.Succeed(null, Group);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetListGroupsAsync));
            throw;
        }
    }

}