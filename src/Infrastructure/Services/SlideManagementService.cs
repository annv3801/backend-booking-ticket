using Application.Commands.Slide;
using Application.Common.Interfaces;
using Application.DataTransferObjects.Slide.Requests;
using Application.DataTransferObjects.Slide.Responses;
using Application.Interface;
using Application.Queries.Slide;
using Application.Repositories.Slide;
using Application.Services.Slide;
using AutoMapper;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nobi.Core.Responses;

// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract


namespace Infrastructure.Services;

public class SlideManagementService : ISlideManagementService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ILoggerService _loggerService;
    private readonly ISlideRepository _slideRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly IFileService _fileService;

    public SlideManagementService(IMapper mapper, IMediator mediator, ILoggerService loggerService, ISlideRepository slideRepository,
        IDateTimeService dateTimeService, ICurrentAccountService currentAccountService, IFileService fileService)
    {
        _mapper = mapper;
        _mediator = mediator;
        _loggerService = loggerService;
        _slideRepository = slideRepository;
        _dateTimeService = dateTimeService;
        _currentAccountService = currentAccountService;
        _fileService = fileService;
    }

    public async Task<RequestResult<bool>> CreateSlideAsync(CreateSlideRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var image = "";
            if (request.Image != null)
            {
                var fileResult = _fileService.SaveImage(request.Image);
                if (fileResult.Item1 == 1)
                {
                    image = fileResult.Item2; 
                }
            }
            
            // Create Slide 
            var slideEntity = _mapper.Map<SlideEntity>(request);

            slideEntity.CreatedBy = _currentAccountService.Id;
            slideEntity.CreatedTime = _dateTimeService.NowUtc;
            slideEntity.Image = image;

            var resultCreateSlide = await _mediator.Send(new CreateSlideCommand {Entity = slideEntity}, cancellationToken);
            if (resultCreateSlide <= 0)
                return RequestResult<bool>.Fail("Save data failed");
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateSlideAsync));
            throw;
        }
    }

    public async Task<RequestResult<bool>> UpdateSlideAsync(UpdateSlideRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var existedSlide = await _slideRepository.GetSlideEntityByIdAsync(request.Id, cancellationToken);
            if (existedSlide == null)
                return RequestResult<bool>.Fail("Slide is not found");
            
            var currentImage = existedSlide.Image;
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
            
            // Update value to existed Slide
            existedSlide.Name = request.Name;
            existedSlide.ObjectId = request.ObjectId;
            if(image != "") 
                existedSlide.Image = image;
            
            var resultUpdateSlide = await _mediator.Send(new UpdateSlideCommand
            {
                Request = existedSlide,
            }, cancellationToken);
            if (resultUpdateSlide <= 0)
                return RequestResult<bool>.Fail("Save data failed");
           
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(UpdateSlideAsync));
            throw;
        }
    }

    public async Task<RequestResult<bool>> DeleteSlideAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            // Check for existence
            var slideToDelete = await _slideRepository.GetSlideByIdAsync(id, cancellationToken);
            if (slideToDelete == null)
                return RequestResult<bool>.Fail("Slide is not found");

            var resultDeleteSlide = await _mediator.Send(new DeleteSlideCommand
            {
                Id = id,
            }, cancellationToken);
            if (resultDeleteSlide <= 0)
                return RequestResult<bool>.Fail("Save data failed");
           
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(DeleteSlideAsync));
            throw;
        }
    }

    public async Task<RequestResult<SlideResponse>> GetSlideAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var slide = await _mediator.Send(new GetSlideByIdQuery
            {
                Id = id,
            }, cancellationToken);
            if (slide == null)
                return RequestResult<SlideResponse>.Fail("Slide is not found");

            return RequestResult<SlideResponse>.Succeed(null, _mapper.Map<SlideResponse>(slide));
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetSlideAsync));
            throw;
        }
    }

    public async Task<RequestResult<OffsetPaginationResponse<SlideResponse>>> GetListSlidesAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // check tenant valid
            var slide = await _mediator.Send(new GetListSlidesQuery
            {
                OffsetPaginationRequest = request,
            }, cancellationToken);

            return RequestResult<OffsetPaginationResponse<SlideResponse>>.Succeed(null, slide);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetListSlidesAsync));
            throw;
        }
    }
}