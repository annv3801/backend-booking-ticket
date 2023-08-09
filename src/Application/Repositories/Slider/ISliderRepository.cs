using Application.Common.Interfaces;
using Application.DataTransferObjects.Slider.Requests;

namespace Application.Repositories.Slider;

public interface ISliderRepository : IRepository<Domain.Entities.Slider>
{
    Task<Domain.Entities.Slider?> GetSliderByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IQueryable<Domain.Entities.Slider>> GetListSlidersAsync(ViewListSlidersRequest request, CancellationToken cancellationToken);
}