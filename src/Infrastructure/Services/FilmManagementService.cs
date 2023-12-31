using System.Security.Claims;
using System.Text;
using Application.Commands.Film;
using Application.Common.Interfaces;
using Application.DataTransferObjects.Film.Requests;
using Application.DataTransferObjects.Film.Responses;
using Application.Interface;
using Application.Queries.Film;
using Application.Repositories.Category;
using Application.Repositories.Film;
using Application.Services.Film;
using AutoMapper;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Domain.Constants;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Nobi.Core.Responses;

// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract


namespace Infrastructure.Services;

public class FilmManagementService : IFilmManagementService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ILoggerService _loggerService;
    private readonly IFilmRepository _filmRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly IFileService _fileService;
    private readonly ISnowflakeIdService _snowflakeIdService;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IFeedbackFilmRepository _feedbackFilmRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FilmManagementService(IMapper mapper, IMediator mediator, ILoggerService loggerService, IFilmRepository filmRepository,
        IDateTimeService dateTimeService, ICurrentAccountService currentAccountService, IFileService fileService, ISnowflakeIdService snowflakeIdService, ICategoryRepository categoryRepository, IFeedbackFilmRepository feedbackFilmRepository, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _mediator = mediator;
        _loggerService = loggerService;
        _filmRepository = filmRepository;
        _dateTimeService = dateTimeService;
        _currentAccountService = currentAccountService;
        _fileService = fileService;
        _snowflakeIdService = snowflakeIdService;
        _categoryRepository = categoryRepository;
        _feedbackFilmRepository = feedbackFilmRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<RequestResult<bool>> CreateFilmAsync(CreateFilmRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // Check duplicated name or code of field
            var existedFilm = await _filmRepository.GetFilmBySlugAsync(request.Slug, cancellationToken);
            if (existedFilm != null)
                return RequestResult<bool>.Fail("Duplicated film name");
            var image = "";
            if (request.Image != null)
            {
                var fileResult = _fileService.SaveImage(request.Image);
                if (fileResult.Item1 == 1)
                {
                    image = fileResult.Item2; // getting name of image
                }
            }

            var stringBuilder = new StringBuilder();
            foreach (var item in request.CategoryIds)
            {
                var categoryItem = await _categoryRepository.GetCategoryByIdAsync(item, cancellationToken);
                if (categoryItem == null)
                    return RequestResult<bool>.Fail("Not Found Category");
                if (stringBuilder.Length > 0)
                    stringBuilder.Append(", ");
                stringBuilder.Append(categoryItem.Name);
            }
            var genre = stringBuilder.ToString();
            var newFilm = new FilmEntity()
            {
                Id = await _snowflakeIdService.GenerateId(cancellationToken),
                Name = request.Name,
                Slug = request.Slug,
                CategoryIds = request.CategoryIds,
                Description = request.Description,
                Director = request.Director,
                Genre = genre,
                Actor = request.Actor,
                Premiere = request.Premiere,
                Duration = request.Duration,
                Language = request.Language,
                Rated = request.Rated,
                Trailer = request.Trailer,
                Image = image,
                GroupEntityId = request.GroupId,
                TotalRating = 0
            };
            var filmEntity = _mapper.Map<FilmEntity>(newFilm);
            var result = await _mediator.Send(new CreateFilmCommand{ Entity = filmEntity }, cancellationToken);
            if (result <= 0)
                return RequestResult<bool>.Fail("Save data failed");
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateFilmAsync));
            throw;
        }
    }

    public async Task<RequestResult<bool>> UpdateFilmAsync(UpdateFilmRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var existedFilm = await _filmRepository.GetFilmEntityByIdAsync(request.Id, cancellationToken);
            if (existedFilm == null)
                return RequestResult<bool>.Fail("Film is not found");
    
            var stringBuilder = new StringBuilder();
            foreach (var item in request.CategoryIds)
            {
                var categoryItem = await _categoryRepository.GetCategoryByIdAsync(item, cancellationToken);
                if (categoryItem == null)
                    return RequestResult<bool>.Fail("Not Found Category");
                if (stringBuilder.Length > 0)
                    stringBuilder.Append(", ");
                stringBuilder.Append(categoryItem.Name);
            }
            var genre = stringBuilder.ToString();
            
            var currentImage = existedFilm.Image; // Load current image name/path from the database or wherever you store it
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
            
            // Update value to existed Film
            existedFilm.Name = request.Name;
            existedFilm.Slug = request.Slug;
            existedFilm.CategoryIds = request.CategoryIds;
            existedFilm.Description = request.Description;
            existedFilm.Director = request.Director;
            existedFilm.Genre = genre;
            existedFilm.Actor = request.Actor;
            existedFilm.Premiere = request.Premiere;
            existedFilm.Duration = request.Duration;
            existedFilm.Language = request.Language;
            existedFilm.Rated = request.Rated;
            existedFilm.Trailer = request.Trailer;
            existedFilm.Image = image;
            existedFilm.GroupEntityId = request.GroupId;
    
            var resultUpdateFilm = await _mediator.Send(new UpdateFilmCommand
            {
                Request = existedFilm,
            }, cancellationToken);
            if (resultUpdateFilm <= 0)
                return RequestResult<bool>.Fail("Save data failed");
    
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(UpdateFilmAsync));
            throw;
        }
    }
    
    public async Task<RequestResult<bool>> DeleteFilmAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            // Check for existence
            var filmToDelete = await _filmRepository.GetFilmEntityByIdAsync(id, cancellationToken);
            if (filmToDelete == null)
                return RequestResult<bool>.Fail("Film is not found");
    
            var resultDeleteFilm = await _mediator.Send(new DeleteFilmCommand
            {
                Id = id,
            }, cancellationToken);
            if (resultDeleteFilm <= 0)
                return RequestResult<bool>.Fail("Save data failed");
    
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(DeleteFilmAsync));
            throw;
        }
    }
    
    public async Task<RequestResult<FilmResponse>> GetFilmAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            long.TryParse(_httpContextAccessor.HttpContext?.User.FindFirstValue(JwtClaimTypes.UserId), out var userId);
            var accountId = (long)0;
            if (userId != 0) 
                accountId = userId; 
            var film = await _mediator.Send(new GetFilmByIdQuery
            {
                Id = id,
                AccountId = accountId
            }, cancellationToken);
            if (film == null)
                return RequestResult<FilmResponse>.Fail("Film is not found");
            var filmFeedbackResponse = await _feedbackFilmRepository.Entity.Where(x => x.FilmId == id).GroupBy(x => x.FilmId).Select(x => new FeedbackFilmResponse()
            {
                FilmId = id,
                AverageRating = (double)x.Sum(xx => xx.Rating) / x.Count(),
                CountOneStar = x.Count(xx => xx.Rating == 1),
                CountTwoStar = x.Count(xx => xx.Rating == 2),
                CountThreeStar = x.Count(xx => xx.Rating == 3),
                CountFourStar = x.Count(xx => xx.Rating == 4),
                CountFiveStar = x.Count(xx => xx.Rating == 5),
                CountStart = x.Count()
            }).FirstOrDefaultAsync(cancellationToken);
            film.FeedbackFilmResponse = filmFeedbackResponse;
            return RequestResult<FilmResponse>.Succeed(null, _mapper.Map<FilmResponse>(film));
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetFilmAsync));
            throw;
        }
    }
    
    public async Task<RequestResult<OffsetPaginationResponse<FilmResponse>>> GetListFilmsByGroupAsync(ViewListFilmsByGroupRequest request, CancellationToken cancellationToken)
    {
        try
        {
            long.TryParse(_httpContextAccessor.HttpContext?.User.FindFirstValue(JwtClaimTypes.UserId), out var userId);
            var accountId = (long)0;
            if (userId != 0) 
                accountId = userId; 
            var film = await _mediator.Send(new GetListFilmsByGroupQuery
            {
                Request = request,
                AccontId = accountId
            }, cancellationToken);
    
            return RequestResult<OffsetPaginationResponse<FilmResponse>>.Succeed(null, film);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetListFilmsByGroupAsync));
            throw;
        }
    }

    public async Task<RequestResult<OffsetPaginationResponse<FilmResponse>>> GetListFilmsAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // check tenant valid
            var film = await _mediator.Send(new GetListFilmsQuery()
            {
                Request = request,
            }, cancellationToken);
    
            return RequestResult<OffsetPaginationResponse<FilmResponse>>.Succeed(null, film);
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(GetListFilmsAsync));
            throw;
        }
    }

    public async Task<RequestResult<bool>> CreateFeedbackFilmAsync(CreateFeedbackFilmRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var existedFeedbackFilm = await _feedbackFilmRepository.GetFeedbackByAccountIdAndFilmIdAsync(_currentAccountService.Id, request.FilmId, cancellationToken);
            if (existedFeedbackFilm)
                return RequestResult<bool>.Fail("Feedback for this film is valid");
            
            var newFilmFeedback = new FilmFeedbackEntity()
            {
                Id = await _snowflakeIdService.GenerateId(cancellationToken),
                AccountId = _currentAccountService.Id,
                FilmId = request.FilmId,
                Rating = request.Rating,
                CreatedBy = _currentAccountService.Id,
                CreatedTime = _dateTimeService.NowUtc,
            };
            var filmFeedbackEntity = _mapper.Map<FilmFeedbackEntity>(newFilmFeedback);
            var result = await _mediator.Send(new CreateFeedbackFilmCommand{ Entity = filmFeedbackEntity }, cancellationToken);
            if (result <= 0)
                return RequestResult<bool>.Fail("Save data failed");
            return RequestResult<bool>.Succeed("Save data success");
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateFeedbackFilmAsync));
            throw;
        }
    }

}