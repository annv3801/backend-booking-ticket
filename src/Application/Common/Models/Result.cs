using System.Diagnostics.CodeAnalysis;

namespace Application.Common.Models;
[ExcludeFromCodeCoverage]
public class Result<TDto> where TDto : class
{
    internal Result(bool success, IEnumerable<ErrorItem> errors, TDto dto, bool forbidden)
    {
        Succeeded = success;
        Errors = errors.ToArray();
        Data = dto;
        Forbidden = forbidden;
    }

    public TDto Data { get; private set; }
    public bool Succeeded { get; private set; }
    public bool Forbidden { get; private set; }

    public IEnumerable<ErrorItem> Errors { get; private set; }

    public static Result<TDto> Succeed(TDto data = null)
    {
        return new Result<TDto>(true, new ErrorItem[] { }, data,false);
    }

    public static Result<TDto> Fail(IEnumerable<ErrorItem> errors, TDto data = null)
    {
        return new Result<TDto>(false, errors, data,false);
    }

    public static Result<TDto> Forbid(IEnumerable<ErrorItem> errors, TDto data = null)
    {
        return new Result<TDto>(false, errors, data,true);
    }
}