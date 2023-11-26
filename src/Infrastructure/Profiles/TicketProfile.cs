using Application.Commands.Ticket;
using Application.DataTransferObjects.Ticket.Requests;
using Application.DataTransferObjects.Ticket.Responses;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Profiles.Account.Resolvers;

namespace Infrastructure.Profiles;

public class TicketProfile : Profile
{
    public TicketProfile()
    {
        // Create
        CreateMap<CreateTicketRequest, CreateTicketCommand>().ReverseMap();
        CreateMap<CreateTicketRequest, TicketEntity>()
            .ForMember(d => d.Id, o => o.MapFrom<IdGeneratorResolver>());

        // Update 
        CreateMap<UpdateTicketRequest, UpdateTicketCommand>().ReverseMap();


        CreateMap<TicketEntity, TicketResponse>()
            ;
    }
}