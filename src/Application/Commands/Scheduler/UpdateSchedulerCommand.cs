using Domain.Entities;
using MediatR;

namespace Application.Commands.Scheduler;

public class UpdateSchedulerCommand : IRequest<int>
{
    public required SchedulerEntity Request { get; set; }
}