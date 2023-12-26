using MediatR;

namespace Application.Commands.Scheduler;

public class DeleteSchedulerCommand : IRequest<int>
{
    public long Id { get; set; }
}