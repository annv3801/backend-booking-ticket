using Domain.Entities;
using MediatR;

namespace Application.Commands.Scheduler;

public class CreateSchedulerCommand : IRequest<int>
{
    public SchedulerEntity Entity { get; set; }
}