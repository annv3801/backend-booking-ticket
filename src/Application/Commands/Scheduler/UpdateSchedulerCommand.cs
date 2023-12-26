using Domain.Entities;
using MediatR;

namespace Application.Commands.Scheduler;

public class UpdateSchedulerCommand : IRequest<int>
{
    public SchedulerEntity Request { get; set; }
}