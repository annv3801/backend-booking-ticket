using Application.Commands.Category;
using Application.Commands.Film;
using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Requests;
using Application.DataTransferObjects.Category.Requests;
using Application.DataTransferObjects.Category.Responses;
using Application.DataTransferObjects.Film.Requests;
using Application.DataTransferObjects.Film.Responses;
using Application.DataTransferObjects.Pagination.Responses;
using Application.Queries.Film;
using Application.Repositories.Category;
using Application.Repositories.Film;
using Application.Services.Category;
using Application.Services.Film;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Infrastructure.Services;

public class FilmManagementService : IFilmManagementService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IFilmRepository _filmRepository;
    private readonly IStringLocalizationService _localizationService;
    public readonly ICurrentAccountService CurrentAccountService;
    private readonly IPaginationService _paginationService;
    private readonly IFileService _fileService;

    public FilmManagementService(IMediator mediator, IMapper mapper, IFilmRepository filmRepository, IStringLocalizationService localizationService, ICurrentAccountService currentAccountService, IPaginationService paginationService, IFileService fileService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _filmRepository = filmRepository;
        _localizationService = localizationService;
        CurrentAccountService = currentAccountService;
        _paginationService = paginationService;
        _fileService = fileService;
    }
    
    public async Task<Result<FilmResult>> CreateFilmAsync(CreateFilmRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Check duplicated name or code of field
            var existedFilm = await _filmRepository.GetFilmByShortenUrlAsync(request.ShortenUrl, cancellationToken);
            if (existedFilm != null)
                return Result<FilmResult>.Fail(LocalizationString.Category.DuplicateCategory.ToErrors(_localizationService));
            var image = "";
            if (request.Image != null)
            {
                var fileResult = _fileService.SaveImage(request.Image);
                if (fileResult.Item1 == 1)
                {
                    image = fileResult.Item2; // getting name of image
                }
            }
            var newFilm = new Film()
            {
                Id = new Guid(),
                Name = request.Name,
                ShortenUrl = request.ShortenUrl,
                CategoryId = request.CategoryId,
                Description = request.Description,
                Director = request.Director,
                Genre = request.Genre,
                Actor = request.Actor,
                Premiere = request.Premiere,
                Duration = request.Duration,
                Language = request.Language,
                Rated = request.Rated,
                Trailer = request.Trailer,
                Image = image,
                Status = request.Status
            };

            var result = await _mediator.Send(new CreateFilmCommand(newFilm), cancellationToken);
            return result > 0 ? Result<FilmResult>.Succeed(_mapper.Map<FilmResult>(newFilm)) : Result<FilmResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<ViewFilmResponse>> ViewFilmAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _filmRepository.GetFilmByIdAsync(id, cancellationToken);
            if (result == null)
                return Result<ViewFilmResponse>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            var filmResult = _mapper.Map<ViewFilmResponse>(result);

            return Result<ViewFilmResponse>.Succeed(filmResult);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<FilmResult>> DeleteFilmAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Check for existence
            var filmToDelete = await _filmRepository.GetFilmByIdAsync(id, cancellationToken);
            if (filmToDelete == null)
                return Result<FilmResult>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            filmToDelete.Status = 0;
            filmToDelete.LastModifiedById = CurrentAccountService.Id;
            filmToDelete.LastModified = DateTime.Now;

            var resultDeleteFilm = await _mediator.Send(new DeleteFilmCommand(filmToDelete), cancellationToken);
            return resultDeleteFilm <= 0 ? Result<FilmResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService)) : Result<FilmResult>.Succeed(_mapper.Map<FilmResult>(filmToDelete));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<FilmResult>> UpdateFilmAsync(Guid id, UpdateFilmRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find role
            var existedFilm = await _filmRepository.GetFilmByIdAsync(id, cancellationToken);
            if (existedFilm == null)
                return Result<FilmResult>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            // Check duplicated shorten url
            var categoryValid = await _filmRepository.GetFilmByShortenUrlAsync(request.ShortenUrl, cancellationToken);
            if (categoryValid != null)
                return Result<FilmResult>.Fail(LocalizationString.Category.DuplicateCategory.ToErrors(_localizationService));

            existedFilm.Name = request.Name;
            existedFilm.ShortenUrl = request.ShortenUrl;
            existedFilm.CategoryId = request.CategoryId;
            existedFilm.Description = request.Description;
            existedFilm.Director = request.Director;
            existedFilm.Actor = request.Actor;
            existedFilm.Genre = request.Genre;
            existedFilm.Premiere = request.Premiere;
            existedFilm.Duration = request.Duration;
            existedFilm.Language = request.Language;
            existedFilm.Rated = request.Rated;
            existedFilm.Trailer = request.Trailer;
            existedFilm.Status = request.Status;
            existedFilm.LastModified = DateTime.Now;
            existedFilm.LastModifiedById = CurrentAccountService.Id;

            var resultUpdateFilm = await _mediator.Send(new UpdateFilmCommand(existedFilm), cancellationToken);
            return resultUpdateFilm <= 0 ? Result<FilmResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService)) : Result<FilmResult>.Succeed(_mapper.Map<FilmResult>(existedFilm));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<PaginationBaseResponse<ViewFilmResponse>>> ViewListFilmsAsync(ViewListFilmsRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var source = await _filmRepository.GetListFilmsAsync(request, cancellationToken);
            var result = await _paginationService.PaginateAsync(source, request.Page, request.OrderBy, request.OrderByDesc, request.Size, cancellationToken);
            if (result.Result.Count == 0)
            {
                return Result<PaginationBaseResponse<ViewFilmResponse>>.Fail(
                    _localizationService[LocalizationString.Category.FailedToViewList].Value.ToErrors(_localizationService));
            }
            return Result<PaginationBaseResponse<ViewFilmResponse>>.Succeed(new PaginationBaseResponse<ViewFilmResponse>()
            {
                CurrentPage = result.CurrentPage,
                OrderBy = result.OrderBy,
                OrderByDesc = result.OrderByDesc,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                Result = result.Result.Select(a => new ViewFilmResponse()
                {
                    Id = a.Id,
                    Name = a.Name,
                    ShortenUrl = a.ShortenUrl,
                    Description = a.Description,
                    CategoryId = a.CategoryId,
                    Director = a.Director,
                    Actor = a.Actor,
                    Genre = a.Genre,
                    Premiere = a.Premiere,
                    Duration = a.Duration,
                    Language = a.Language,
                    Rated = a.Rated,
                    Image = a.Image
                }).ToList()
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<ViewFilmResponse>> ViewFilmByShortenUrlAsync(string shortenUrl, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _filmRepository.GetFilmByShortenUrlAsync(shortenUrl, cancellationToken);
            if (result == null)
                return Result<ViewFilmResponse>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            var filmResult = _mapper.Map<ViewFilmResponse>(result);

            return Result<ViewFilmResponse>.Succeed(filmResult);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Result<PaginationBaseResponse<ViewFilmResponse>>> ViewListFilmByCategoryAsync(ViewListFilmByCategoryQuery query, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var filterQuery = await _filmRepository.ViewListFilmByCategoryAsync(query, cancellationToken);
            var source = filterQuery.Select(p => new {p.Name, p.Image, p.ShortenUrl, p.Id, p.CategoryId, p.Actor, p.Director, p.Duration, p.Genre, p.Language, p.Premiere, p.Rated, p.Description});
            var result = await _paginationService.PaginateAsync(source, query.Page, query.OrderBy, query.OrderByDesc, query.Size, cancellationToken);
            if (result.Result.Count == 0)
            {
                return Result<PaginationBaseResponse<ViewFilmResponse>>.Fail(
                    _localizationService[LocalizationString.Film.FailedToViewList].Value.ToErrors(_localizationService));
            }
            return Result<PaginationBaseResponse<ViewFilmResponse>>.Succeed(new PaginationBaseResponse<ViewFilmResponse>()
            {
                CurrentPage = result.CurrentPage,
                OrderBy = result.OrderBy,
                OrderByDesc = result.OrderByDesc,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                Result = result.Result.Select(a => new ViewFilmResponse()
                {
                    Id = a.Id,
                    Name = a.Name,
                    ShortenUrl = a.ShortenUrl,
                    Description = a.Description,
                    CategoryId = a.CategoryId,
                    Director = a.Director,
                    Genre = a.Genre,
                    Actor = a.Actor,
                    Premiere = a.Premiere,
                    Duration = a.Duration,
                    Language = a.Language,
                    Rated = a.Rated,
                    Image = a.Image
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