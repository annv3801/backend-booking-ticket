using Domain.Entities;
using MediatR;

namespace Application.Commands.Scheduler;

public class CreateSchedulerCommand : IRequest<int>
{
    public required SchedulerEntity Entity { get; set; }
}