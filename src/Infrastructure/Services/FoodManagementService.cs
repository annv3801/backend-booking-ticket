using Application.Commands.Food;
using Application.Common.Interfaces;
using Application.DataTransferObjects.Food.Requests;
using Application.DataTransferObjects.Food.Responses;
using Application.Interface;
using Application.Queries.Food;
using Application.Repositories.Food;
using Application.Services.Food;
using AutoMapper;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Domain.Entities;
using MediatR;
using Nobi.Core.Responses;

// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract


namespace Infrastructure.Services;

public class FoodManagementService : IFoodManagementService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ILoggerService _loggerService;
    private readonly IFoodRepository _foodRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly IFileService _fileService;
    public FoodManagementService(IMapper mapper, IMediator mediator, ILoggerService loggerService, IFoodRepository foodRepository,
        IDateTimeService dateTimeService, ICurrentAccountService currentAccountService, IFileService fileService)
    {
        _mapper = mapper;
        _mediator = mediator;
        _loggerService = loggerService;
        _foodRepository = foodRepository;
        _dateTimeService = dateTimeService;
        _currentAccountService = currentAccountService;
        _fileService = fileService;
    }

    public async Task<RequestResult<bool>> CreateFoodAsync(CreateFoodRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // Check duplicate Food name
            if (await _mediator.Send(new CheckDuplicatedFoodByNameQuery
                {
                    Title = request.Title,
                }, cancellationToken))
                return RequestResult<bool>.Fail("Item is duplicated");

            var image = "";
            if (request.Image != null)
            {
                var fileResult = _fileService.SaveImage(request.Image);
                if (fileResult.Item1 == 1)
                {
                    image = fileResult.Item2; // getting name of image
                }
            }
            
            // Create Food 
            var foodEntity = _mapper.Map<FoodEntity>(request);

            foodEntity.CreatedBy = _currentAccountService.Id;
            foodEntity.CreatedTime = _dateTimeService.NowUtc;
            foodEntity.Image = image;

            var resultCreateFood = await _mediator.Send(new CreateFoodCommand {Entity = foodEntity}, cancellationToken);
            if (resultCreateFood <= 0)
                return RequestResult<bool>.Fail("Save data failed");
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateFoodAsync));
            throw;
        }
    }

    public async Task<RequestResult<bool>> UpdateFoodAsync(UpdateFoodRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // Check duplicate Food name
            if (await _mediator.Send(new CheckDuplicatedFoodByNameAndIdQuery
                {
                    Title = request.Title,
                    Id = request.Id,
                }, cancellationToken))
                return RequestResult<bool>.Fail("Item is duplicated");
            
            var existedFood = await _foodRepository.GetFoodEntityByIdAsync(request.Id, cancellationToken);
            if (existedFood == null)
                return RequestResult<bool>.Fail("Food is not found");
            
            var currentImage = existedFood.Image; // Load current image name/path from the database or wherever you store it
            var image = "";

            if (request.Image != null)
            {
                // Delete the current image if it exists
                if (!string.IsNullOrEmpty(currentImage))
                {
                    _fileService.DeleteImage(currentImage);
                }

                // Save the new image
                var fileResult = _fileService.SaveImage(request.Image);
                if (fileResult.Item1 == 1)
                {
                    image = fileResult.Item2; // getting name of new image

                    // Save this new image name/path to the database or wherever you store it
                }
            }
            
            // Update value to existed Food
            existedFood.Title = request.Title;
            existedFood.Image = image;

            var resultUpdateFood = await _mediator.Send(new UpdateFoodCommand
            {
                Request = existedFood,
            }, cancellationToken);
            if (resultUpdateFood <= 0)
                return RequestResult<bool>.Fail("Save data failed");
           
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(UpdateFoodAsync));
            throw;
        }
    }

    public async Task<RequestResult<bool>> DeleteFoodAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            // Check for existence
            var foodToDelete = await _foodRepository.GetFoodByIdAsync(id, cancellationToken);
            if (foodToDelete == null)
                return RequestResult<bool>.Fail("Food is not found");


            var resultDeleteFood = await _mediator.Send(new DeleteFoodCommand
            {
                Id = id,
            }, cancellationToken);
            if (resultDeleteFood <= 0)
                return RequestResult<bool>.Fail("Save data failed");
           
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(DeleteFoodAsync));
            throw;
        }
    }

    public async Task<RequestResult<FoodResponse>> GetFoodAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var food = await _mediator.Send(new GetFoodByIdQuery
            {
                Id = id,
            }, cancellationToken);
            if (food == null)
                return RequestResult<FoodResponse>.Fail("Food is not found");

            return RequestResult<FoodResponse>.Succeed(null, _mapper.Map<FoodResponse>(food));
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetFoodAsync));
            throw;
        }
    }

    public async Task<RequestResult<OffsetPaginationResponse<FoodResponse>>> GetListFoodsAsync(ViewFoodRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // check tenant valid
            var food = await _mediator.Send(new GetListFoodsQuery
            {
                Request = request,
            }, cancellationToken);

            return RequestResult<OffsetPaginationResponse<FoodResponse>>.Succeed(null, food);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetListFoodsAsync));
            throw;
        }
    }

}