using System.Diagnostics.CodeAnalysis;

namespace Domain.Exceptions;
[ExcludeFromCodeCoverage]
public class AccountNotFoundException : Exception
{
    public AccountNotFoundException(string? accountId) : base(accountId)
    {
    }

    public AccountNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
