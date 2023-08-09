using Application.Common.Models;

namespace Application.Common.Interfaces;
public interface IPasswordGeneratorService
{
    public string GenerateRandomPassword(PasswordOptions opts = null!);
    public string HashPassword(string password);
    public bool VerifyHashPassword(string? hashedPassword, string password);
}
