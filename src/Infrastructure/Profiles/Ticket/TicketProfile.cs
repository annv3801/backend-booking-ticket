using Application.Common;
using Application.DataTransferObjects.Ticket.Responses;
using AutoMapper;

namespace Infrastructure.Profiles.Ticket;

public class TicketProfile : Profile
{
    public TicketProfile()
    {
        CreateMap<Domain.Entities.Ticket, ViewTicketResponse>().ReverseMap();
        CreateMap<Domain.Entities.Ticket, TicketResult>().ReverseMap();
    }
}