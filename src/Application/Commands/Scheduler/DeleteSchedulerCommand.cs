using MediatR;

namespace Application.Commands.Scheduler;

public class DeleteSchedulerCommand : IRequest<int>
{
    public required long Id { get; set; }
}