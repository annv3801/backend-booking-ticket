using Application.DataTransferObjects.Ticket.Responses;
using Application.Interface;
using Application.Repositories.Ticket;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Constants;
using Domain.Entities;
using Domain.Extensions;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Ticket;

public class TicketRepository : Repository<TicketEntity, ApplicationDbContext>, ITicketRepository
{
    private readonly DbSet<TicketEntity> _ticketEntities;
    private readonly IMapper _mapper;

    public TicketRepository(ApplicationDbContext applicationDbContext, IMapper mapper, ISnowflakeIdService snowflakeIdService) : base(applicationDbContext, snowflakeIdService)
    {
        _mapper = mapper;
        _ticketEntities = applicationDbContext.Set<TicketEntity>();
    }

    public async Task<OffsetPaginationResponse<TicketResponse>> GetListTicketsAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        var query = _ticketEntities.Where(x => !x.Deleted).OrderBy(x => x.Title.ToLower()).Select(x => new TicketResponse()
            {
                Title = x.Title,
                Price = x.Price,
                Status = x.Status,
                Id = x.Id,
                Type = x.Type,
                Color = x.Color,
                CreatedTime = x.CreatedTime
            });
        
        var response = await query.PaginateAsync<TicketEntity,TicketResponse>(request, cancellationToken);
        return new OffsetPaginationResponse<TicketResponse>()
        {
            Data = response.Data,
            PageSize = response.PageSize,
            Total = response.Total,
            CurrentPage = response.CurrentPage
        };
    }

    public async Task<TicketResponse?> GetTicketByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _ticketEntities.AsNoTracking().ProjectTo<TicketResponse>(_mapper.ConfigurationProvider).Where(x => x.Id == id && x.Status != EntityStatus.Deleted).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TicketEntity?> GetTicketEntityByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _ticketEntities.AsNoTracking().Where(x => x.Id == id && x.Status != EntityStatus.Deleted).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> IsDuplicatedTicketByNameAndIdAsync(string name, long id, CancellationToken cancellationToken)
    {
        return await _ticketEntities.AsNoTracking().AnyAsync(x => x.Title == name && x.Id != id && !x.Deleted, cancellationToken);
    }

    public async Task<bool> IsDuplicatedTicketByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _ticketEntities.AsNoTracking().AnyAsync(x => x.Title == name && !x.Deleted, cancellationToken);
    }
    
}