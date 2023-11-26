using Application.Commands.Ticket;
using Application.Common.Interfaces;
using Application.DataTransferObjects.Ticket.Requests;
using Application.DataTransferObjects.Ticket.Responses;
using Application.Interface;
using Application.Queries.Ticket;
using Application.Repositories.Ticket;
using Application.Services.Ticket;
using AutoMapper;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Domain.Entities;
using MediatR;
using Nobi.Core.Responses;

// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract


namespace Infrastructure.Services;

public class TicketManagementService : ITicketManagementService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ILoggerService _loggerService;
    private readonly ITicketRepository _ticketRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentAccountService _currentAccountService;

    public TicketManagementService(IMapper mapper, IMediator mediator, ILoggerService loggerService, ITicketRepository ticketRepository,
        IDateTimeService dateTimeService, ICurrentAccountService currentAccountService)
    {
        _mapper = mapper;
        _mediator = mediator;
        _loggerService = loggerService;
        _ticketRepository = ticketRepository;
        _dateTimeService = dateTimeService;
        _currentAccountService = currentAccountService;
    }

    public async Task<RequestResult<bool>> CreateTicketAsync(CreateTicketRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // Check duplicate Ticket name
            if (await _mediator.Send(new CheckDuplicatedTicketByNameQuery
                {
                    Title = request.Title,
                }, cancellationToken))
                return RequestResult<bool>.Fail("Item is duplicated");

            // Create Ticket 
            var ticketEntity = _mapper.Map<TicketEntity>(request);

            ticketEntity.CreatedBy = _currentAccountService.Id;
            ticketEntity.CreatedTime = _dateTimeService.NowUtc;

            var resultCreateTicket = await _mediator.Send(new CreateTicketCommand {Entity = ticketEntity}, cancellationToken);
            if (resultCreateTicket <= 0)
                return RequestResult<bool>.Fail("Save data failed");
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateTicketAsync));
            throw;
        }
    }

    public async Task<RequestResult<bool>> UpdateTicketAsync(UpdateTicketRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // Check duplicate Ticket name
            if (await _mediator.Send(new CheckDuplicatedTicketByNameAndIdQuery
                {
                    Title = request.Title,
                    Id = request.Id,
                }, cancellationToken))
                return RequestResult<bool>.Fail("Item is duplicated");


            var existedTicket = await _ticketRepository.GetTicketEntityByIdAsync(request.Id, cancellationToken);
            if (existedTicket == null)
                return RequestResult<bool>.Fail("Ticket is not found");

            
            // Update value to existed Ticket
            existedTicket.Title = request.Title;

            var resultUpdateTicket = await _mediator.Send(new UpdateTicketCommand
            {
                Request = existedTicket,
            }, cancellationToken);
            if (resultUpdateTicket <= 0)
                return RequestResult<bool>.Fail("Save data failed");
           
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(UpdateTicketAsync));
            throw;
        }
    }

    public async Task<RequestResult<bool>> DeleteTicketAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            // Check for existence
            var ticketToDelete = await _ticketRepository.GetTicketByIdAsync(id, cancellationToken);
            if (ticketToDelete == null)
                return RequestResult<bool>.Fail("Ticket is not found");


            var resultDeleteTicket = await _mediator.Send(new DeleteTicketCommand
            {
                Id = id,
            }, cancellationToken);
            if (resultDeleteTicket <= 0)
                return RequestResult<bool>.Fail("Save data failed");
           
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(DeleteTicketAsync));
            throw;
        }
    }

    public async Task<RequestResult<TicketResponse>> GetTicketAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var ticket = await _mediator.Send(new GetTicketByIdQuery
            {
                Id = id,
            }, cancellationToken);
            if (ticket == null)
                return RequestResult<TicketResponse>.Fail("Ticket is not found");

            return RequestResult<TicketResponse>.Succeed(null, _mapper.Map<TicketResponse>(ticket));
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetTicketAsync));
            throw;
        }
    }

    public async Task<RequestResult<OffsetPaginationResponse<TicketResponse>>> GetListTicketsAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // check tenant valid
            var ticket = await _mediator.Send(new GetListTicketsQuery
            {
                OffsetPaginationRequest = request,
            }, cancellationToken);

            return RequestResult<OffsetPaginationResponse<TicketResponse>>.Succeed(null, ticket);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetListTicketsAsync));
            throw;
        }
    }

}