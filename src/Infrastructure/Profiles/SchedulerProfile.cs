using Application.DataTransferObjects.Scheduler.Requests;
using Application.DataTransferObjects.Scheduler.Responses;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Profiles;

public class SchedulerProfile: Profile
{
    public SchedulerProfile()
    {
        CreateMap<SchedulerEntity, SchedulerResponse>();
        CreateMap<CreateSchedulerRequest, SchedulerEntity>();
    }
}