using Application.Commands.Category;
using Application.Commands.Slider;
using Application.Commands.Theater;
using Application.Commands.Ticket;
using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Requests;
using Application.DataTransferObjects.Category.Requests;
using Application.DataTransferObjects.Category.Responses;
using Application.DataTransferObjects.Pagination.Responses;
using Application.DataTransferObjects.Slider.Requests;
using Application.DataTransferObjects.Slider.Responses;
using Application.DataTransferObjects.Theater.Requests;
using Application.DataTransferObjects.Theater.Responses;
using Application.DataTransferObjects.Ticket.Requests;
using Application.DataTransferObjects.Ticket.Responses;
using Application.Repositories.Category;
using Application.Repositories.Slider;
using Application.Repositories.Theater;
using Application.Repositories.Ticket;
using Application.Services.Category;
using Application.Services.Slider;
using Application.Services.Theater;
using Application.Services.Ticket;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Infrastructure.Services;

public class SliderManagementService : ISliderManagementService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ISliderRepository _sliderRepository;
    private readonly IStringLocalizationService _localizationService;
    public readonly ICurrentAccountService CurrentAccountService;
    private readonly IPaginationService _paginationService;


    public SliderManagementService(IMediator mediator, IMapper mapper, ISliderRepository sliderRepository, IStringLocalizationService localizationService, ICurrentAccountService currentAccountService, IPaginationService paginationService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _sliderRepository = sliderRepository;
        _localizationService = localizationService;
        CurrentAccountService = currentAccountService;
        _paginationService = paginationService;
    }
    
    public async Task<Result<SliderResult>> CreateSliderAsync(CreateSliderRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var newField = new Slider()
            {
                Id = new Guid(),
                ImageFile = request.Image,
                Status = request.Status
            };

            var result = await _mediator.Send(new CreateSliderCommand(newField), cancellationToken);
            return result > 0 ? Result<SliderResult>.Succeed(_mapper.Map<SliderResult>(newField)) : Result<SliderResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<ViewSliderResponse>> ViewSliderAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _sliderRepository.GetSliderByIdAsync(id, cancellationToken);
            if (result == null)
                return Result<ViewSliderResponse>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            var theaterResult = _mapper.Map<ViewSliderResponse>(result);

            return Result<ViewSliderResponse>.Succeed(theaterResult);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<PaginationBaseResponse<ViewSliderResponse>>> ViewListSlidersAsync(ViewListSlidersRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var source = await _sliderRepository.GetListSlidersAsync(request, cancellationToken);
            var result = await _paginationService.PaginateAsync(source, request.Page, request.OrderBy, request.OrderByDesc, request.Size, cancellationToken);
            if (result.Result.Count == 0)
            {
                return Result<PaginationBaseResponse<ViewSliderResponse>>.Fail(
                    _localizationService[LocalizationString.Category.FailedToViewList].Value.ToErrors(_localizationService));
            }
            return Result<PaginationBaseResponse<ViewSliderResponse>>.Succeed(new PaginationBaseResponse<ViewSliderResponse>()
            {
                CurrentPage = result.CurrentPage,
                OrderBy = result.OrderBy,
                OrderByDesc = result.OrderByDesc,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                Result = result.Result.Select(a => new ViewSliderResponse()
                {
                    Id = a.Id,
                    Image = a.Image,
                    Status = a.Status
                }).ToList()
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}