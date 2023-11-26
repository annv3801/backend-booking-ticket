using Application.Commands.Ticket;
using Application.Common.Interfaces;
using Application.Handlers.Ticket;
using Application.Interface;
using Application.Repositories.Ticket;
using Domain.Common.Interface;
using Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Handlers.Commands.Ticket;

public class TicketCommandHandler : ICreateTicketCommandHandler, IUpdateTicketCommandHandler, IDeleteTicketCommandHandler
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILoggerService _loggerService;
    private readonly ICurrentAccountService _currentAccountService;

    public TicketCommandHandler(ITicketRepository ticketRepository, IDateTimeService dateTimeService, ILoggerService loggerService, ICurrentAccountService currentAccountService)
    {
        _ticketRepository = ticketRepository;
        _dateTimeService = dateTimeService;
        _loggerService = loggerService;
        _currentAccountService = currentAccountService;
    }

    public async Task<int> Handle(CreateTicketCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _ticketRepository.AddAsync(command.Entity, cancellationToken);
            return await _ticketRepository.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(Handle));
            throw;
        }
    }

    public async Task<int> Handle(UpdateTicketCommand command, CancellationToken cancellationToken)
    {
        try
        {
            return await _ticketRepository.Entity.Where(x => x.Id == command.Request.Id && x.Status != EntityStatus.Deleted)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(l => l.Title, command.Request.Title)
                    .SetProperty(l => l.Type, command.Request.Type)
                    .SetProperty(l => l.Price, command.Request.Price)
                    .SetProperty(l => l.ModifiedBy, _currentAccountService.Id)
                    .SetProperty(l => l.ModifiedTime, _dateTimeService.NowUtc), cancellationToken);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(Handle));
            throw;
        }
    }

    public async Task<int> Handle(DeleteTicketCommand command, CancellationToken cancellationToken)
    {
        try
        {
            return await _ticketRepository.Entity.Where(x => x.Id == command.Id && x.Status != EntityStatus.Deleted)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(l => l.DeletedTime, _dateTimeService.NowUtc)
                    .SetProperty(l => l.DeletedBy, _currentAccountService.Id)
                    .SetProperty(l => l.Deleted, true)
                    .SetProperty(l => l.Status, EntityStatus.Deleted), cancellationToken);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(Handle));
            throw;
        }
    }
}