using Application.Commands.Category;
using Application.Commands.Ticket;
using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Requests;
using Application.DataTransferObjects.Category.Requests;
using Application.DataTransferObjects.Category.Responses;
using Application.DataTransferObjects.Pagination.Responses;
using Application.DataTransferObjects.Ticket.Requests;
using Application.DataTransferObjects.Ticket.Responses;
using Application.Repositories.Category;
using Application.Repositories.Ticket;
using Application.Services.Category;
using Application.Services.Ticket;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Infrastructure.Services;

public class TicketManagementService : ITicketManagementService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ITicketRepository _ticketRepository;
    private readonly IStringLocalizationService _localizationService;
    public readonly ICurrentAccountService CurrentAccountService;
    private readonly IPaginationService _paginationService;


    public TicketManagementService(IMediator mediator, IMapper mapper, ITicketRepository ticketRepository, IStringLocalizationService localizationService, ICurrentAccountService currentAccountService, IPaginationService paginationService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _ticketRepository = ticketRepository;
        _localizationService = localizationService;
        CurrentAccountService = currentAccountService;
        _paginationService = paginationService;
    }
    
    public async Task<Result<TicketResult>> CreateTicketAsync(CreateTicketRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var newField = new Ticket()
            {
                Id = new Guid(),
                Name = request.Name,
                ScheduleId = request.ScheduleId,
                Price = request.Price,
                Type = request.Type
            };

            var result = await _mediator.Send(new CreateTicketCommand(newField), cancellationToken);
            return result > 0 ? Result<TicketResult>.Succeed(_mapper.Map<TicketResult>(newField)) : Result<TicketResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<ViewTicketResponse>> ViewTicketAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _ticketRepository.GetTicketByIdAsync(id, cancellationToken);
            if (result == null)
                return Result<ViewTicketResponse>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            var ticketResult = _mapper.Map<ViewTicketResponse>(result);

            return Result<ViewTicketResponse>.Succeed(ticketResult);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<TicketResult>> DeleteTicketAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Check for existence
            var ticketToDelete = await _ticketRepository.GetTicketByIdAsync(id, cancellationToken);
            if (ticketToDelete == null)
                return Result<TicketResult>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            ticketToDelete.Status = 0;
            ticketToDelete.LastModifiedById = CurrentAccountService.Id;
            ticketToDelete.LastModified = DateTime.Now;

            var resultDeleteTicket = await _mediator.Send(new DeleteTicketCommand(ticketToDelete), cancellationToken);
            return resultDeleteTicket <= 0 ? Result<TicketResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService)) : Result<TicketResult>.Succeed(_mapper.Map<TicketResult>(ticketToDelete));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<TicketResult>> UpdateTicketAsync(Guid id, UpdateTicketRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find role
            var existedTicket = await _ticketRepository.GetTicketByIdAsync(id, cancellationToken);
            if (existedTicket == null)
                return Result<TicketResult>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            existedTicket.Name = request.Name;
            existedTicket.Price = request.Price;
            existedTicket.Type = request.Type;
            existedTicket.LastModified = DateTime.Now;
            existedTicket.LastModifiedById = CurrentAccountService.Id;

            var resultUpdateTicket = await _mediator.Send(new UpdateTicketCommand(existedTicket), cancellationToken);
            return resultUpdateTicket <= 0 ? Result<TicketResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService)) : Result<TicketResult>.Succeed(_mapper.Map<TicketResult>(existedTicket));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<PaginationBaseResponse<ViewTicketResponse>>> ViewListTicketsAsync(ViewListTicketsRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var source = await _ticketRepository.GetListTicketsAsync(request, cancellationToken);
            var result = await _paginationService.PaginateAsync(source, request.Page, request.OrderBy, request.OrderByDesc, request.Size, cancellationToken);
            if (result.Result.Count == 0)
            {
                return Result<PaginationBaseResponse<ViewTicketResponse>>.Fail(
                    _localizationService[LocalizationString.Category.FailedToViewList].Value.ToErrors(_localizationService));
            }
            return Result<PaginationBaseResponse<ViewTicketResponse>>.Succeed(new PaginationBaseResponse<ViewTicketResponse>()
            {
                CurrentPage = result.CurrentPage,
                OrderBy = result.OrderBy,
                OrderByDesc = result.OrderByDesc,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                Result = result.Result.Select(a => new ViewTicketResponse()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Price = a.Price,
                    Type = a.Type,
                    ScheduleId = a.ScheduleId
                }).ToList()
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}