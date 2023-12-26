using MediatR;

namespace Application.Queries.Theater;

public class CheckDuplicatedTheaterByNameQuery :  IRequest<bool>
{
    public string Name { get; set; }
}