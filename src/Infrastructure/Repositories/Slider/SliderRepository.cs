using Application.DataTransferObjects.Slider.Requests;
using Application.Repositories.Slider;
using AutoMapper;
using Infrastructure.Common.Repositories;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Slider;

public class SliderRepository : Repository<Domain.Entities.Slider>, ISliderRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;
    
    public SliderRepository(ApplicationDbContext applicationDbContext, IMapper mapper) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<Domain.Entities.Slider?> GetSliderByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Sliders.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IQueryable<Domain.Entities.Slider>> GetListSlidersAsync(ViewListSlidersRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return _applicationDbContext.Sliders
            .AsSplitQuery()
            .AsQueryable();
    }
    
}