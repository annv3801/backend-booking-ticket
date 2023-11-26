﻿using Application.Commands.Theater;
using Application.Common.Interfaces;
using Application.DataTransferObjects.Theater.Requests;
using Application.DataTransferObjects.Theater.Responses;
using Application.Interface;
using Application.Queries.Scheduler;
using Application.Queries.Theater;
using Application.Repositories.Theater;
using Application.Services.Theater;
using AutoMapper;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nobi.Core.Responses;

// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract


namespace Infrastructure.Services;

public class TheaterManagementService : ITheaterManagementService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ILoggerService _loggerService;
    private readonly ITheaterRepository _theaterRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly ISnowflakeIdService _snowflakeIdService;

    public TheaterManagementService(IMapper mapper, IMediator mediator, ILoggerService loggerService, ITheaterRepository theaterRepository,
        IDateTimeService dateTimeService, ICurrentAccountService currentAccountService, ISnowflakeIdService snowflakeIdService)
    {
        _mapper = mapper;
        _mediator = mediator;
        _loggerService = loggerService;
        _theaterRepository = theaterRepository;
        _dateTimeService = dateTimeService;
        _currentAccountService = currentAccountService;
        _snowflakeIdService = snowflakeIdService;
    }

    public async Task<RequestResult<bool>> CreateTheaterAsync(CreateTheaterRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // Check duplicate Theater name
            if (await _mediator.Send(new CheckDuplicatedTheaterByNameQuery
                {
                    Name = request.Name,
                }, cancellationToken))
                return RequestResult<bool>.Fail("Item is duplicated");

            // Create Theater 
            var theaterEntity = _mapper.Map<TheaterEntity>(request);

            theaterEntity.Id = await _snowflakeIdService.GenerateId(cancellationToken);
            theaterEntity.CreatedBy = _currentAccountService.Id;
            theaterEntity.CreatedTime = _dateTimeService.NowUtc;

            var resultCreateTheater = await _mediator.Send(new CreateTheaterCommand {Entity = theaterEntity}, cancellationToken);
            if (resultCreateTheater <= 0)
                return RequestResult<bool>.Fail("Save data failed");
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateTheaterAsync));
            throw;
        }
    }

    public async Task<RequestResult<bool>> UpdateTheaterAsync(UpdateTheaterRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // Check duplicate Theater name
            if (await _mediator.Send(new CheckDuplicatedTheaterByNameAndIdQuery
                {
                    Name = request.Name,
                    Id = request.Id,
                }, cancellationToken))
                return RequestResult<bool>.Fail("Item is duplicated");


            var existedTheater = await _theaterRepository.GetTheaterEntityByIdAsync(request.Id, cancellationToken);
            if (existedTheater == null)
                return RequestResult<bool>.Fail("Theater is not found");

            
            // Update value to existed Theater
            existedTheater.Name = request.Name;
            existedTheater.Location = request.Location;
            existedTheater.Longitude = request.Longitude;
            existedTheater.Latitude = request.Latitude;
            existedTheater.PhoneNumber = request.PhoneNumber;

            var resultUpdateTheater = await _mediator.Send(new UpdateTheaterCommand
            {
                Request = existedTheater,
            }, cancellationToken);
            if (resultUpdateTheater <= 0)
                return RequestResult<bool>.Fail("Save data failed");
           
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(UpdateTheaterAsync));
            throw;
        }
    }

    public async Task<RequestResult<bool>> DeleteTheaterAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            // Check for existence
            var theaterToDelete = await _theaterRepository.GetTheaterByIdAsync(id, cancellationToken);
            if (theaterToDelete == null)
                return RequestResult<bool>.Fail("Theater is not found");


            var resultDeleteTheater = await _mediator.Send(new DeleteTheaterCommand
            {
                Id = id,
            }, cancellationToken);
            if (resultDeleteTheater <= 0)
                return RequestResult<bool>.Fail("Save data failed");
           
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(DeleteTheaterAsync));
            throw;
        }
    }

    public async Task<RequestResult<TheaterResponse>> GetTheaterAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var theater = await _mediator.Send(new GetTheaterByIdQuery
            {
                Id = id,
            }, cancellationToken);
            if (theater == null)
                return RequestResult<TheaterResponse>.Fail("Theater is not found");

            return RequestResult<TheaterResponse>.Succeed(null, _mapper.Map<TheaterResponse>(theater));
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetTheaterAsync));
            throw;
        }
    }

    public async Task<RequestResult<OffsetPaginationResponse<TheaterResponse>>> GetListTheatersAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // check tenant valid
            var theater = await _mediator.Send(new GetListTheatersQuery
            {
                OffsetPaginationRequest = request,
            }, cancellationToken);

            return RequestResult<OffsetPaginationResponse<TheaterResponse>>.Succeed(null, theater);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetListTheatersAsync));
            throw;
        }
    }

}