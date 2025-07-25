using DTOs;
using Microsoft.AspNetCore.Identity;
namespace Helper;

public static class PasswordHelper
{
  public static string HashPassword(string plainPassword)
    {
        var hasher = new PasswordHasher<UserDto>();
        return hasher.HashPassword(null!, plainPassword); // null porque no necesitas una instancia real para el hash
    }

    public static bool VerifyPassword(string hashedPassword, string plainPassword)
    {
        var hasher = new PasswordHasher<UserDto>();
        var result = hasher.VerifyHashedPassword(null!, hashedPassword, plainPassword);
        return result == PasswordVerificationResult.Success;
    }
}
