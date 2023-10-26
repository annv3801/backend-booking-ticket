namespace Domain.Exceptions;

public class InvalidPaginationParameterException : ExceptionBase
{
    public InvalidPaginationParameterException(string context, string key, string message) : base(context, key, message)
    {
    }

    public InvalidPaginationParameterException(string context, string key, string message, Exception exception) : base(context, key, message, exception)
    {
    }
}