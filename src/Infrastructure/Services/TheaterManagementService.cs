using Application.Commands.Category;
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
using Application.DataTransferObjects.Theater.Requests;
using Application.DataTransferObjects.Theater.Responses;
using Application.DataTransferObjects.Ticket.Requests;
using Application.DataTransferObjects.Ticket.Responses;
using Application.Repositories.Category;
using Application.Repositories.Theater;
using Application.Repositories.Ticket;
using Application.Services.Category;
using Application.Services.Theater;
using Application.Services.Ticket;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Infrastructure.Services;

public class TheaterManagementService : ITheaterManagementService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ITheaterRepository _theaterRepository;
    private readonly IStringLocalizationService _localizationService;
    public readonly ICurrentAccountService CurrentAccountService;
    private readonly IPaginationService _paginationService;


    public TheaterManagementService(IMediator mediator, IMapper mapper, ITheaterRepository theaterRepository, IStringLocalizationService localizationService, ICurrentAccountService currentAccountService, IPaginationService paginationService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _theaterRepository = theaterRepository;
        _localizationService = localizationService;
        CurrentAccountService = currentAccountService;
        _paginationService = paginationService;
    }
    
    public async Task<Result<TheaterResult>> CreateTheaterAsync(CreateTheaterRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var newField = new Theater()
            {
                Id = new Guid(),
                Name = request.Name,
                Address = request.Address,
                Status = request.Status
            };

            var result = await _mediator.Send(new CreateTheaterCommand(newField), cancellationToken);
            return result > 0 ? Result<TheaterResult>.Succeed(_mapper.Map<TheaterResult>(newField)) : Result<TheaterResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<ViewTheaterResponse>> ViewTheaterAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _theaterRepository.GetTheaterByIdAsync(id, cancellationToken);
            if (result == null)
                return Result<ViewTheaterResponse>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            var theaterResult = _mapper.Map<ViewTheaterResponse>(result);

            return Result<ViewTheaterResponse>.Succeed(theaterResult);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<TheaterResult>> DeleteTheaterAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Check for existence
            var theaterToDelete = await _theaterRepository.GetTheaterByIdAsync(id, cancellationToken);
            if (theaterToDelete == null)
                return Result<TheaterResult>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            theaterToDelete.Status = 0;
            theaterToDelete.LastModifiedById = CurrentAccountService.Id;
            theaterToDelete.LastModified = DateTime.Now;

            var resultDeleteTicket = await _mediator.Send(new DeleteTheaterCommand(theaterToDelete), cancellationToken);
            return resultDeleteTicket <= 0 ? Result<TheaterResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService)) : Result<TheaterResult>.Succeed(_mapper.Map<TheaterResult>(theaterToDelete));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<TheaterResult>> UpdateTheaterAsync(Guid id, UpdateTheaterRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find Theater
            var existedTheater = await _theaterRepository.GetTheaterByIdAsync(id, cancellationToken);
            if (existedTheater == null)
                return Result<TheaterResult>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            existedTheater.Name = request.Name;
            existedTheater.Address = request.Address;
            existedTheater.Status = request.Status;
            existedTheater.LastModified = DateTime.Now;
            existedTheater.LastModifiedById = CurrentAccountService.Id;

            var resultUpdateTheater = await _mediator.Send(new UpdateTheaterCommand(existedTheater), cancellationToken);
            return resultUpdateTheater <= 0 ? Result<TheaterResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService)) : Result<TheaterResult>.Succeed(_mapper.Map<TheaterResult>(existedTheater));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<PaginationBaseResponse<ViewTheaterResponse>>> ViewListTheatersAsync(ViewListTheatersRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var source = await _theaterRepository.GetListTheatersAsync(request, cancellationToken);
            var result = await _paginationService.PaginateAsync(source, request.Page, request.OrderBy, request.OrderByDesc, request.Size, cancellationToken);
            if (result.Result.Count == 0)
            {
                return Result<PaginationBaseResponse<ViewTheaterResponse>>.Fail(
                    _localizationService[LocalizationString.Category.FailedToViewList].Value.ToErrors(_localizationService));
            }
            return Result<PaginationBaseResponse<ViewTheaterResponse>>.Succeed(new PaginationBaseResponse<ViewTheaterResponse>()
            {
                CurrentPage = result.CurrentPage,
                OrderBy = result.OrderBy,
                OrderByDesc = result.OrderByDesc,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                Result = result.Result.Select(a => new ViewTheaterResponse()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Address = a.Address,
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