using Application.Common.Interfaces;
using Application.DataTransferObjects.Theater.Requests;

namespace Application.Repositories.Theater;

public interface ITheaterRepository : IRepository<Domain.Entities.Theater>
{
    Task<Domain.Entities.Theater?> GetTheaterByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IQueryable<Domain.Entities.Theater>> GetListTheatersAsync(ViewListTheatersRequest request, CancellationToken cancellationToken);
}