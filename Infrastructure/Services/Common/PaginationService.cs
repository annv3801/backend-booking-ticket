using System.Diagnostics.CodeAnalysis;
using System.Linq.Dynamic.Core;
using Application.Common;
using Application.Common.Interfaces;
using Application.DataTransferObjects.Pagination.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Common;
[ExcludeFromCodeCoverage]
public class PaginationService : IPaginationService
{
    private readonly ILogger<PaginationService> _logger;

    public PaginationService(ILogger<PaginationService> logger)
    {
        _logger = logger;
    }

    public async Task<PaginationBaseResponse<T>> PaginateAsync<T>(IQueryable<T> source, int page, string? orderBy, bool orderByDesc, int pageSize, CancellationToken cancellationToken = default(CancellationToken)) where T : class
    {
        if (page == 0) page = Constants.Pagination.DefaultPage;
        if (pageSize == 0) pageSize = Constants.Pagination.DefaultSize;
        var paginationResponse = new PaginationBaseResponse<T>
        {
            TotalPages = (int) Math.Ceiling((double) source.Count() / pageSize),
            TotalItems = source.Count(),
            PageSize = pageSize,
            CurrentPage = page,
            OrderBy = orderBy,
            OrderByDesc = orderByDesc
        };

        var skip = (page - 1) * pageSize;
        var props = typeof(T).GetProperties();
        //var orderByProperty = props.FirstOrDefault(n => n.GetCustomAttribute<SortableAttribute>()?.OrderBy.ToLower() == orderBy?.ToLower() && orderBy != null);


        // if (orderBy != null && orderByProperty == null)
        //     throw new NotSortableFieldException($"Field: '{orderBy}' is not sortable");


        var sortRequired = orderBy != null;
        var order = orderByDesc ? "DESC" : "ASC";
        orderBy = orderBy?.ToLower();

        if (sortRequired)
        {
            try
            {
                paginationResponse.Result = await source
                    .OrderBy($"{orderBy} {order}") //Use Linq Dynamic Core
                    .Skip(skip)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, "Error while converting to list async, trying to list sync");
                paginationResponse.Result = source
                    .OrderBy($"{orderBy} {order}") //Use Linq Dynamic Core
                    .Skip(skip)
                    .Take(pageSize)
                    .ToList();
                Console.WriteLine("??");
                throw;
            }
        }
        else
        {
            try
            {
                paginationResponse.Result = await source
                    .Skip(skip)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, "Error while converting to list async, trying to list sync");
                paginationResponse.Result = source
                    .Skip(skip)
                    .Take(pageSize)
                    .ToList();
                throw;
            }
        }


        return paginationResponse;
    }

    public async Task<PaginationBaseResponse<T>> PaginateWithSortManyFieldAsync<T>(IQueryable<T> source, int page, string? orderBy, bool orderByDesc, bool orderByDifotDesc, bool orderByQaqcDesc, int pageSize, CancellationToken cancellationToken = default(CancellationToken)) where T : class
    {
         if (page == 0) page = Constants.Pagination.DefaultPage;
        if (pageSize == 0) pageSize = Constants.Pagination.DefaultSize;
        var paginationResponse = new PaginationBaseResponse<T>
        {
            TotalPages = (int) Math.Ceiling((double) source.Count() / pageSize),
            TotalItems = source.Count(),
            PageSize = pageSize,
            CurrentPage = page,
            OrderBy = orderBy,
            OrderByDesc = orderByDesc
        };

        var skip = (page - 1) * pageSize;
        var props = typeof(T).GetProperties();
        //var orderByProperty = props.FirstOrDefault(n => n.GetCustomAttribute<SortableAttribute>()?.OrderBy.ToLower() == orderBy?.ToLower() && orderBy != null);


        // if (orderBy != null && orderByProperty == null)
        //     throw new NotSortableFieldException($"Field: '{orderBy}' is not sortable");


        var sortRequired = orderBy != null;
        var order = orderByDesc ? "DESC" : "ASC";
        var orderDifot = orderByDifotDesc ? "DESC" : "ASC";
        var orderQaqc = orderByQaqcDesc ? "DESC" : "ASC";
        orderBy = orderBy?.ToLower();
        var orderByDifot = "DIFOT";
        var orderByQAQC = "QAQC";
        orderBy = orderBy?.ToLower();

        if (sortRequired)
        {
            try
            {
                paginationResponse.Result = await source
                    .OrderBy($"{orderBy} {order}")//Use Linq Dynamic Core
                    .ThenBy($"{orderByDifot} {orderDifot}") 
                    .ThenBy($"{orderByQAQC} {orderQaqc}") 
                    .Skip(skip)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, "Error while converting to list async, trying to list sync");
                paginationResponse.Result = source
                    .OrderBy($"{orderBy} {order}") //Use Linq Dynamic Core
                    .ThenBy($"{orderByDifot} {orderDifot}") 
                    .ThenBy($"{orderByQAQC} {orderQaqc}")
                    .Skip(skip)
                    .Take(pageSize)
                    .ToList();
                Console.WriteLine("??");
                throw;
            }
        }
        else
        {
            try
            {
                paginationResponse.Result = await source
                    .Skip(skip)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, "Error while converting to list async, trying to list sync");
                paginationResponse.Result = source
                    .Skip(skip)
                    .Take(pageSize)
                    .ToList();
                throw;
            }
        }


        return paginationResponse;
    }

    public async Task<PaginationBaseResponse<T>> PaginateWithListAsync<T>(IEnumerable<T> source, int page, string? orderBy, bool orderByDesc, int pageSize, CancellationToken cancellationToken = default(CancellationToken)) where T : class
    {
        if (page == 0) page = Constants.Pagination.DefaultPage;
        if (pageSize == 0) pageSize = Constants.Pagination.DefaultSize;
        var paginationResponse = new PaginationBaseResponse<T>
        {
            TotalPages = (int) Math.Ceiling((double) source.Count() / pageSize),
            TotalItems = source.Count(),
            PageSize = pageSize,
            CurrentPage = page,
            OrderBy = orderBy,
            OrderByDesc = orderByDesc
        };

        var skip = (page - 1) * pageSize;
        var props = typeof(T).GetProperties();
        var orderByProperty = props.FirstOrDefault(n => n.Name.ToLower() == orderBy?.ToLower());
        
        var sortRequired = orderBy != null;
        // var order = orderByDesc ? "DESC" : "ASC";
        // orderBy = orderBy?.ToLower();
        if (sortRequired)
        {
            try
            {
                if (orderByDesc)
                {
                    paginationResponse.Result = source
                        .OrderByDescending(x=>orderByProperty.GetValue(x,null)) 
                        .Skip(skip)
                        .Take(pageSize)
                        .ToList();
                }
                else
                {
                    paginationResponse.Result = source
                        .OrderBy(x=>orderByProperty.GetValue(x,null)) 
                        .Skip(skip)
                        .Take(pageSize)
                        .ToList();
                }
              
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, "Error while converting to list async, trying to list sync");
                paginationResponse.Result = source
                    .OrderByDescending(x=>orderByProperty.GetValue(x,null)) 
                    .Skip(skip)
                    .Take(pageSize)
                    .ToList();
                Console.WriteLine("??");
                throw;
            }
        }
        else
        {
            try
            {
                paginationResponse.Result =  source
                    .Skip(skip)
                    .Take(pageSize)
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, "Error while converting to list async, trying to list sync");
                paginationResponse.Result = source
                    .Skip(skip)
                    .Take(pageSize)
                    .ToList();
                throw;
            }
        }


        return paginationResponse;
    }
}
