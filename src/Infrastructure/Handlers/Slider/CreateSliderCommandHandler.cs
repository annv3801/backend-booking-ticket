using Application.Commands.Slider;
using Application.Handlers.Slider;
using Application.Repositories.Slider;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.Slider;

public class CreateSliderCommandHandler : ICreateSliderCommandHandler
{
    private readonly ISliderRepository _sliderRepository;
    private readonly ApplicationDbContext _applicationDbContext;

    public CreateSliderCommandHandler(ISliderRepository sliderRepository, ApplicationDbContext applicationDbContext)
    {
        _sliderRepository = sliderRepository;
        _applicationDbContext = applicationDbContext;
    }


    public async Task<int> Handle(CreateSliderCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _sliderRepository.AddAsync(command.Entity, cancellationToken);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}