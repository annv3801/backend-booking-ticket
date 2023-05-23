using Application.Commands.Slider;
using Application.Handlers.Slider;
using Application.Repositories.Slider;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.Slider;

public class DeleteSliderCommandHandler : IDeleteSliderCommandHandler
{
    private readonly ISliderRepository _sliderRepository;
    private readonly ApplicationDbContext _applicationDbContext;

    public DeleteSliderCommandHandler(ISliderRepository sliderRepository, ApplicationDbContext applicationDbContext)
    {
        _sliderRepository = sliderRepository;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<int> Handle(DeleteSliderCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _sliderRepository.Update(command.Entity);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}