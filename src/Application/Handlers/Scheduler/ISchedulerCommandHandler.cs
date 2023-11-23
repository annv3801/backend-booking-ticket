using Application.Commands.Scheduler;
using MediatR;

namespace Application.Handlers.Scheduler;

public interface ICreateSchedulerCommandHandler : IRequestHandler<CreateSchedulerCommand, int>
{
}

public interface IUpdateSchedulerCommandHandler : IRequestHandler<UpdateSchedulerCommand, int>
{
}

public interface IDeleteSchedulerCommandHandler : IRequestHandler<DeleteSchedulerCommand, int>
{
}
